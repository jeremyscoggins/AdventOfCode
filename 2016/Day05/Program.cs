using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day05
{
    class Program
    {
        const string input = "ffykfhsq";
        static void Main(string[] args)
        {
            Int64 index = 0;
            string password = "";
            char[] password2 = "________".ToCharArray();
            using (MD5 md5Hash = MD5.Create())
            {
                while (true)
                {
                    var hash = GetMd5Hash(md5Hash, input + index.ToString());
                    if (hash.StartsWith("00000"))
                    {
                        char char5 = hash.Substring(5, 1)[0];

                        if (password.Length < 8)
                        {
                            password += char5;
                            Console.WriteLine("Hash: {0} Input: {1}", hash, input + index.ToString());
                        }

                        int charIndex = Convert.ToInt32(char5.ToString(), 16);
                        if (charIndex < 8 && password2[charIndex] == '_')
                        {
                            char char6 = hash.Substring(6, 1)[0];

                            password2[charIndex] = char6;
                            Console.WriteLine("Password 2: {0} Hash: {1} Input: {2}", new String(password2), hash, input + index.ToString());
                        }


                        if (password.Length == 8 && password2.Contains('_') == false)
                        {
                            break;
                        }
                    }
                    index++;
                }
            }
            Console.WriteLine("Password Is: {0}", password);
            Console.WriteLine("Password2 Is: {0}", new String(password2));
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
    }
}
