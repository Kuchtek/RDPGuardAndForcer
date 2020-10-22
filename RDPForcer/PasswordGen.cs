using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PasswordGenerator;

namespace RDPForcer
{
    class PasswordGen
    {
        static List<string> passwordsUsed = new List<string>();
        public static string Generate(int minLenght = 8, int maxLength = 16)
        {
            var result = "";
            do
            {
                Random r = new Random();
                int len = r.Next(minLenght, maxLength);
                var pwd = new Password().IncludeLowercase().IncludeNumeric().IncludeSpecial().IncludeUppercase().LengthRequired(len);
                result = pwd.Next();
                passwordsUsed.Add(result);
            }
            while (!passwordsUsed.Contains(result));
            return result;
        }
    }
}
