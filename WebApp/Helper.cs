using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;

namespace WebApp
{
    public static class Helper
    {
        public static byte[] Hash(string plantext)
        {
            HashAlgorithm algorithm = HashAlgorithm.Create("SHA-256");
            return algorithm.ComputeHash(Encoding.ASCII.GetBytes(plantext));
        }
        public static string RandomString(int n)
        {
            string s = "qwertyuiopasdfghjklzxcvbnm1234567890";
            List<char> list = new List<char>(n);
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                int index = random.Next(s.Length);
                list.Add(s[index]);
            }
            return string.Join("", list);
        }
    }
}
