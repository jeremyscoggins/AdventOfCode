using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day16
{
    class Program
    {
        const string pattern = "Sue (\\d+): (\\w+): (\\d+), (\\w+): (\\d+), (\\w+): (\\d+)";
        static void Main(string[] args)
        {
            Dictionary<string, int> qualities = new Dictionary<string, int>{
                {"children", 3    },
                {"cats", 7        },
                {"samoyeds", 2    },
                {"pomeranians", 3 },
                {"akitas", 0      },
                {"vizslas", 0     },
                {"goldfish", 5    },
                {"trees", 3       },
                {"cars", 2        },
                {"perfumes", 1}
            };
            List<Dictionary<string, int>> aunts = new List<Dictionary<string, int>>();
            using (var fileStream = File.OpenRead("input.txt"))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        Match match = Regex.Match(line, pattern);
                        int index = int.Parse(match.Groups[1].Value);
                        string key1 = match.Groups[2].Value;
                        int value1 = int.Parse(match.Groups[3].Value);
                        string key2 = match.Groups[4].Value;
                        int value2 = int.Parse(match.Groups[5].Value);
                        string key3 = match.Groups[6].Value;
                        int value3 = int.Parse(match.Groups[7].Value);
                        var dict = new Dictionary<string, int>();
                        dict.Add("index", index);
                        dict.Add(key1, value1);
                        dict.Add(key2, value2);
                        dict.Add(key3, value3);
                        aunts.Add(dict);
                    }
                }
            }
            for (int i = 0; i < aunts.Count; i++)
            {
                var auntQualities = aunts[i];
                int matchScore = 0;
                foreach (var quality in qualities)
                {
                    foreach (var auntQuality in auntQualities)
                    {
                        if (auntQuality.Key == quality.Key)
                        {
                            if (auntQuality.Key == "cats" || auntQuality.Key == "trees")
                            {
                                if (auntQuality.Value > quality.Value)
                                {
                                    matchScore++;
                                }
                            }
                            else if (auntQuality.Key == "pomeranians" || auntQuality.Key == "goldfish")
                            {
                                if (auntQuality.Value < quality.Value)
                                {
                                    matchScore++;
                                }
                            }
                            else if (auntQuality.Value == quality.Value)
                            {
                                matchScore++;
                            }
                        }
                    }
                    
                }
                auntQualities["score"] = matchScore;
            }
            var topTen = aunts.OrderByDescending(a => a["score"]).Take(10).ToArray();
            Console.WriteLine("The aunt that matches the closest is aunt number {0} with a {1} match score", topTen[0]["index"], topTen[0]["score"]);
            Console.ReadLine();
        }
    }
}
