using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DaySeven
{
    enum Operation
    {
        OR,
        AND,
        NOT,
        LSHIFT,
        RSHIFT,
        ASSIGN,
    }

    class Program
    {
        const string pattern = "((([a-z0-9]+) )?([A-Z]+) )?([a-z0-9]+) \\-> ([a-z]+)";
        static Dictionary<string, UInt16> wires = new Dictionary<string, UInt16>();
        static List<Action> actions = new List<Action>();
        static List<Action> inputs = new List<Action>();
        static UInt16 getOrValue(string input)
        {
            UInt16 value;
            if (!UInt16.TryParse(input, out value))
            {
                if (!wires.ContainsKey(input))
                {
                    wires[input] = 0;
                }
                return wires[input];
            }
            return value;
        }

        static void setValue(string input, string value)
        {
            setValue(input, UInt16.Parse(value));
        }

        static void setValue(string input, UInt16 value)
        {
            if (input == "a")
            {
                Console.WriteLine("assign {0} to a", value);
            }
            wires[input] = value;
        }

        static bool isNumeric(string input)
        {
            UInt16 value;
            return UInt16.TryParse(input, out value);
        }

        static void Main(string[] args)
        {
            List<string> lines = new List<string>();
            using (var fileStream = File.OpenRead("input.txt"))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }
            }
            
            HashSet<int> processed = new HashSet<int>();
            while (processed.Count < lines.Count)
            {
                for (int i = 0; i < lines.Count; i++)
                {
                    if (processed.Contains(i))
                    {
                        continue;
                    }

                    var line = lines[i];
                    var matches = Regex.Match(line, pattern);
                    var paramA = matches.Groups[3].Value;
                    var operation = matches.Groups[4].Value;
                    var paramB = matches.Groups[5].Value;
                    var output = matches.Groups[6].Value;
                    Operation op = operation.Length > 0 ? (Operation)Enum.Parse(typeof(Operation), operation) : Operation.ASSIGN;

                    if ((paramA.Length == 0 || (isNumeric(paramA) || wires.ContainsKey(paramA))) && (isNumeric(paramB) || wires.ContainsKey(paramB)))
                    {
                        switch (op)
                        {
                            case Operation.OR:
                                setValue(output, (UInt16)(getOrValue(paramA) | getOrValue(paramB)));
                                break;
                            case Operation.AND:
                                setValue(output, (UInt16)(getOrValue(paramA) & getOrValue(paramB)));
                                break;
                            case Operation.NOT:
                                setValue(output, (UInt16)(~getOrValue(paramB)));
                                break;
                            case Operation.LSHIFT:
                                setValue(output, (UInt16)(getOrValue(paramA) << getOrValue(paramB)));
                                break;
                            case Operation.RSHIFT:
                                setValue(output, (UInt16)(getOrValue(paramA) >> getOrValue(paramB)));
                                break;
                            case Operation.ASSIGN:
                                //override
                                if (output == "b")
                                {
                                    paramB = "956";
                                }
                                setValue(output, getOrValue(paramB));
                                break;
                            default:
                                break;
                        }
                        processed.Add(i);
                    }
                }
            }
            Console.WriteLine("Signal at wire a is {0}", wires["a"]);
            Console.ReadLine();
        }
    }
}
