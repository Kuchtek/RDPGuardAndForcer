using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using NetFwTypeLib;

namespace RDPGuard
{
    class Guard
    {
        public int RdpCounter { set; get; }
        public int Port { set; get; }
        public int AllowedFailedLogons { set; get; }
        public Dictionary<string,int> FailedLogons { set; get; }

        private Dictionary<string, string> subStatusDict;

        private Dictionary<string, string> GetSubStatusDict()
        {
            return subStatusDict;
        }

        private void SetSubStatusDict(Dictionary<string, string> value)
        {
            subStatusDict = value;
        }

        private List<string> blackListIP;

        private List<string> GetBlackListIP()
        {
            return blackListIP;
        }

        private void SetBlackListIP(List<string> value)
        {
            blackListIP = value;
        }

        public Guard()
        {
            this.RdpCounter = 0;
            this.Port = 3389;
            this.AllowedFailedLogons = 5;
            SetSubStatusDict(new Dictionary<string, string>{
                { "0xC0000064", "Użytkownik nie istnieje!" },
                { "0xC000006A", "Użytkownik istnieje, nieprawidłowe hasło" }
            });
            this.SetBlackListIP(new List<string>());
            this.FailedLogons = new Dictionary<string, int>();
        }
        

        public void Start()
        {
            int counter = 0;
            if (CheckPortAvailability())
            {
                SubscribeToEventLog();
                while (true)
                {
                    Console.WriteLine("Czekam na eventy...");
                    Console.WriteLine(String.Format("Ilość prób: {0}", this.RdpCounter));
                    System.Threading.Thread.Sleep(1000);
                    
                    counter++;
                    if(counter % 5 == 0)
                    {
                        ConsumeCounter();
                    }
                }
            }
        }
        private bool CheckPortAvailability()
        {
            bool any = false;
            IPGlobalProperties iPGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            System.Net.IPEndPoint[] tcpConnectionsInfo = iPGlobalProperties.GetActiveTcpListeners();
            foreach (System.Net.IPEndPoint endPoint in tcpConnectionsInfo)
            {
                if (endPoint.Port == this.Port)
                {
                    Console.WriteLine("Prowadzimy nasłuch!");
                    Console.WriteLine(endPoint.Address);
                    Console.WriteLine(endPoint.ToString());
                    any = true;
                }
            }
            return any;
        }

        // do odczytywania aktualnych
        public void SubscribeToEventLog()
        {
            try
            {
                //Dla logu połączeń TCP
                string logType = "Microsoft-Windows-TerminalServices-RemoteConnectionManager/Operational";
                string query = "*[System/EventID=261]";
                //EventLog log = new EventLog("Microsoft-Windows-TerminalServices-RemoteConnectionManager/Operational", "localhost", "TerminalServices-RemoteConnectionManager");
                var elQuery = new EventLogQuery(logType, PathType.LogName, query);
                var elReader = new EventLogReader(elQuery);
                EventLogWatcher watcher = new EventLogWatcher(elQuery);
                watcher.EventRecordWritten += new EventHandler<EventRecordWrittenEventArgs>(EventLogEventReadConnection);
                watcher.Enabled = true;

                //dla logu złych logowań
                logType = "Security";
                query = "*[System/EventID=4625]";
                var elQuery2 = new EventLogQuery(logType, PathType.LogName, query);
                var elReader2 = new EventLogReader(elQuery2);
                EventLogWatcher watcher2 = new EventLogWatcher(elQuery2);
                watcher2.EventRecordWritten += new EventHandler<EventRecordWrittenEventArgs>(EventLogEventReadFailedLogons);
                watcher2.Enabled = true;
            }
            catch (EventLogReadingException e)
            {
                Console.WriteLine(String.Format("Error reading the log: {0}", e.Message));
            }
        }

        private void EventLogEventReadFailedLogons(object sender, EventRecordWrittenEventArgs e)
        {
            if (e.EventRecord != null)
            {
                String[] xPathRef = new string[6];
                xPathRef[0] = "Event/System/TimeCreated/@SystemTime";
                xPathRef[1] = "Event/System/Computer";
                xPathRef[2] = "Event/UserData/EventXML/listenerName";
                xPathRef[3] = "Event/System/EventID";
                xPathRef[4] = "Event/System/Provider/@Name";
                xPathRef[5] = "Event/EventData/Data[@Name='IpAddress']";


                IEnumerable<String> xPathEnum = xPathRef;
                EventLogPropertySelector selector = new EventLogPropertySelector(xPathEnum);
                IList<object> logEventProperties = ((EventLogRecord)e.EventRecord).GetPropertyValues(selector);
                Console.WriteLine(String.Format("Czas utworzenia: {0}", logEventProperties[0]));
                Console.WriteLine(String.Format("Nazwa komputera: {0}", logEventProperties[1]));
                Console.WriteLine(String.Format("Nazwa listenera: {0}", logEventProperties[2]));
                Console.WriteLine(String.Format("Numer eventu: {0}", logEventProperties[3]));
                Console.WriteLine(String.Format("Nazwa providera: {0}", logEventProperties[4]));
                Console.WriteLine(String.Format("Próba połczenia z IP: {0}", logEventProperties[5]));
                
                if (FailedLogons.ContainsKey(logEventProperties[5].ToString()))
                {
                    var ip = logEventProperties[5].ToString();
                    FailedLogons[ip]++;
                    if (FailedLogons[ip] > AllowedFailedLogons)
                    {
                        //idziesz na blacklistę
                        GetBlackListIP().Add(ip);
                        Console.WriteLine(String.Format("Wykryto próbę ataku na RDP na porcie {0}, przeprowadzonych prób: {1}\n Dodaję adres {2} na blacklistę na firewallu.", this.Port, FailedLogons[logEventProperties[5].ToString()], logEventProperties[5].ToString()));
                        //tworenie nowej reguły
                        INetFwRule2 inboundRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwRule"));
                        inboundRule.Enabled = true;
                        //blokowanie ruchu
                        inboundRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
                        inboundRule.Protocol = 6; //TCP
                        inboundRule.LocalPorts = this.Port.ToString();
                        //nazwa ruli
                        inboundRule.Name = String.Format("Block RDP for {0}", ip);
                        inboundRule.Profiles = (int)(FirewallProfiles.Private | FirewallProfiles.Public | FirewallProfiles.Domain);
                        //dodanie adresu do ruli
                        inboundRule.RemoteAddresses = ip;
                        //dodawanie ruli do firewalla
                        INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                        firewallPolicy.Rules.Add(inboundRule);
                    }
                }
                else
                {
                    FailedLogons.Add(logEventProperties[5].ToString(), 1);
                }

            }
            else
            {
                Console.Write("Event był pusty");
            }
        }

        public void EventLogEventReadConnection(object obj, EventRecordWrittenEventArgs arg)
        {
            if (arg.EventRecord != null)
            {
                this.RdpCounter++;
                String[] xPathRef = new string[5];
                xPathRef[0] = "Event/System/TimeCreated/@SystemTime";
                xPathRef[1] = "Event/System/Computer";
                xPathRef[2] = "Event/UserData/EventXML/listenerName";
                xPathRef[3] = "Event/System/EventID";
                xPathRef[4] = "Event/System/Provider/@Name";
                IEnumerable<String> xPathEnum = xPathRef;
                EventLogPropertySelector selector = new EventLogPropertySelector(xPathEnum);
                IList<object> logEventProperties = ((EventLogRecord)arg.EventRecord).GetPropertyValues(selector);
                Console.WriteLine(String.Format("Czas utworzenia: {0}", logEventProperties[0]));
                Console.WriteLine(String.Format("Nazwa komputera: {0}", logEventProperties[1]));
                Console.WriteLine(String.Format("Nazwa listenera: {0}", logEventProperties[2]));
                Console.WriteLine(String.Format("Numer eventu: {0}", logEventProperties[3]));
                Console.WriteLine(String.Format("Nazwa providera: {0}", logEventProperties[4]));
            }
            else
            {
                Console.Write("Event był pusty");
            }
        }

        public void ConsumeCounter()
        {
            if (this.RdpCounter > 0)
            {
                this.RdpCounter--;
            }
        }
    }

    public enum FirewallProfiles
    {
        Domain = 1,
        Private = 2,
        Public = 4
    }
}
