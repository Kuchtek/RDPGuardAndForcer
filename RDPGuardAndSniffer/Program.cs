using System;
using System.Reflection;
namespace RDPGuard
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly assem = Assembly.GetExecutingAssembly();
            AssemblyName assemName = assem.GetName();
            Version ver = assemName.Version;
            Console.WriteLine("{0}, Version {1}", assemName.Name, ver.ToString());
            Guard guard = new Guard();
            if (args.Length > 0)
            {
                guard.ParseParameters(args);
            }
            guard.Start();
        }
    }
}
