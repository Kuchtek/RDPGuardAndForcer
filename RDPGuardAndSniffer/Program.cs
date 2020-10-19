using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDPGuardAndSniffer
{
    class Program
    {
        static void Main(string[] args)
        {
            Guard guard = new Guard();
            guard.Start();
        }
    }
}
