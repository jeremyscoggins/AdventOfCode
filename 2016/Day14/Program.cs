using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    class Program
    {
        static char[] chars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', 'a', 'b', 'c', 'd', 'e', 'f' };
        static string[] triples = chars.Select(c => new String( new[] { c , c , c })).ToArray();
        static void Main(string[] args)
        {
            //var input = "abc";
            var input = "jlmsuwbz";
            var index = 0;

            var keys = new List<string>();
            var maxKeys = 64;

            var hashCache = new Dictionary<string, string>();

            var stretch = 2016;

            //using (MD5 md5Hash = MD5.Create())
            {
                while (true)
                {
                    string hash;
                    string key = input + index.ToString();
                    if (hashCache.ContainsKey(key))
                    {
                        hash = hashCache[key];
                        hashCache.Remove(key);
                    }
                    else
                    {
                        hash = GetMd5Hash(key, stretch);
                    }
                    string triple;
                    if (findTriple(hash, out triple))
                    {
                        //Console.WriteLine("found a triple: {0} {1}", hash, index);
                        for (int i = 1; i <= 1000; i++)
                        {
                            //Parallel.ForEach(Enumerable.Range(1, 1000), i => {
                                var index2 = index + i;
                                var key2 = input + index2.ToString();
                                string hash2;
                                if (hashCache.ContainsKey(key2))
                                {
                                    hash2 = hashCache[key2];
                                }
                                else
                                {
                                    hash2 = GetMd5Hash(key2, stretch);
                                    hashCache[key2] = hash2;
                                }


                                if (hash2.Contains(triple + triple.Substring(0, 2)))
                                {
                                    i = 1001;
                                    keys.Add(hash);
                                    Console.WriteLine("found a triple triple: {0} {1}", hash, index);
                                    //Console.WriteLine("found a triple triple: {0} {1}", hash2, index2);

                                    if (keys.Count == maxKeys)
                                    {
                                        //winner winner chicken dinner
                                        Console.WriteLine("winning hash: {0} {1}", hash, index);
                                        return;
                                    }
                                }
                            //});
                        }
                    }
                    index++;
                }
            }
        }

        static string GetMd5Hash(string input, int stretch)
        {
            var md5Hash = MD5.Create();
            //var md5Hash = MD5Managed.Create();
            var currentInput = input;
            while (stretch >= 0)
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(currentInput));

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
                currentInput = sBuilder.ToString();
                stretch--;
            }
            return currentInput;
        }

        static bool findTriple(string input, out string triple)
        {
            for (int i = 0; i < input.Length - 2; i++)
            {
                var substr = input.Substring(i, 3);
                foreach (var trip in triples)
                {
                    if (substr == trip)
                    {
                        triple = trip;
                        return true;
                    }
                }
            }
            triple = null;
            return false;
        }

    }
}
