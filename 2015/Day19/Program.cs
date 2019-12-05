using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day19
{
    class Program
    {
        const string pattern = "([A-Ze][a-z]?) \\=> (([A-Z][a-z]?)+)";
        const string pattern2 = "([A-Z][a-z]?)";
        static void Main(string[] args)
        {
            List<Tuple<string, string>> replacements = new List<Tuple<string, string>>();
            string molecule = "";
            using (var fileStream = File.OpenRead("input.txt"))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    uint y = 1;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        if (Regex.IsMatch(line, pattern))
                        {
                            Match match = Regex.Match(line, pattern);
                            string input = match.Groups[1].Value;
                            string output = match.Groups[2].Value;
                            replacements.Add(Tuple.Create(input, output));
                        }
                        else if (line.Length > 0)
                        {
                            molecule = line;
                        }
                    }
                }
            }
            HashSet<string> molecules = new HashSet<string>();
            { 
                MatchCollection matches = Regex.Matches(molecule, pattern2);
                foreach (Match match in matches)
                {
                    Group group = match.Groups[1];
                    foreach (var replacement in replacements.Where(r => r.Item1 == group.Value))
                    {
                        string newMolecule = molecule.Substring(0, group.Index) + replacement.Item2 + molecule.Substring(group.Index + group.Length);
                        molecules.Add(newMolecule);
                    }
                }
            }

            Console.WriteLine("{0} distinct molecules can be created from the source molecule", molecules.Count);

            string current = molecule;
            List<string> moleculeStages = new List<string>();
            List<string> moleculeSteps = new List<string>();
            moleculeStages.Add(molecule);
            var orderedReplacements = replacements.OrderByDescending(m => m.Item2.Length).ToArray();
            while (current != "e")
            {
                bool found = false;
                foreach (var replacement in orderedReplacements)
                {
                    var match = Regex.Match(current, replacement.Item2);
                    if (match.Success)
                    {
                        string newMolecule = current.Substring(0, match.Index) + replacement.Item1 + current.Substring(match.Index + match.Length);
                        moleculeStages.Add(newMolecule);
                        moleculeSteps.Add(replacement.Item1 + " =>" + replacement.Item2);
                        current = newMolecule;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    break;
                }
            }

            Console.WriteLine("the shortest molecule we can arrive at is ({0}) with {1} steps", current, moleculeSteps.Count);

            Console.ReadLine();
        }
    }
}
