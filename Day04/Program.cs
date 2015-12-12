using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DayFour
{
    class Program
    {
        public static string ToHex(byte[] bytes, bool upperCase)
        {
            StringBuilder result = new StringBuilder(bytes.Length * 2);

            for (int i = 0; i < bytes.Length; i++)
                result.Append(bytes[i].ToString(upperCase ? "X2" : "x2"));

            return result.ToString();
        }

        public static string GetMD5String(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            using (MD5 md5 = MD5.Create())
            {
                byte[] md5Bytes = md5.ComputeHash(inputBytes);
                return ToHex(md5Bytes, true);
            }
        }

        static void Main(string[] args)
        {
            int i = 0;
            bool found5 = false;
            bool found6 = false;
            while (true)
            {
                string inputString = "ckczppom" + i.ToString();
                //Task.Run(() =>
                //{
                    string md5string = GetMD5String(inputString);
                    if (!found5 && md5string.StartsWith("00000"))
                    {
                        found5 = true;
                        Console.WriteLine(inputString + " : " + md5string);
                    }
                    if (!found6 && md5string.StartsWith("000000"))
                    {
                        found6 = true;
                        Console.WriteLine(inputString + " : " + md5string);
                    }
                //});
                if (found5 && found6)
                {
                    Console.WriteLine("--DONE--");
                    break;
                }
                i++;
            }
            Console.ReadLine();
        }
    }
}
