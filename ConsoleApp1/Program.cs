using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
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
        static void Main(string[] args)
        {
            string filename = "asdasd.jpg";
            Console.WriteLine(filename.Substring(filename.LastIndexOf('.')));
            Console.WriteLine(RandomString(32) + filename.Substring(filename.LastIndexOf('.')));
        }
    }
}
