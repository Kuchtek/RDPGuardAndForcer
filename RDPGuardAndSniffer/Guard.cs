using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Diagnostics.Eventing.Reader;

namespace RDPGuardAndSniffer
{
    class Guard
    {
        public int RdpCounter { set; get; }
        public int Port { set; get; }
        public int FailedLogons { set; get; }
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
                watcher.EventRecordWritten += new EventHandler<EventRecordWrittenEventArgs>(EventLogEventRead);
                watcher.Enabled = true;

                //dla logu złych logowań
                logType = "Security";
                query = "*[System/EventID=4625]";
                elQuery = new EventLogQuery(logType, PathType.LogName, query);
                elReader = new EventLogReader(elQuery);
                EventLogWatcher watcher2 = new EventLogWatcher(elQuery);
                watcher2.EventRecordWritten += new EventHandler<EventRecordWrittenEventArgs>(EventLogEventRead);
                watcher2.Enabled = true;
            }
            catch (EventLogReadingException e)
            {
                Console.WriteLine(String.Format("Error reading the log: {0}", e.Message));
            }
        }

        public void EventLogEventRead(object obj, EventRecordWrittenEventArgs arg)
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
}
