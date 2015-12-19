using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day15
{
    class Ingredient
    {
        public string Name;
        public int Capacity;
        public int Durability;
        public int Flavor;
        public int Texture;
        public int Calories;
    }
    class Recipie
    {
        public int[] Quantities;
        public int Capacity;
        public int Durability;
        public int Flavor;
        public int Texture;
        public int Calories;
        public int Score;
    }

    class Program
    {
        const string pattern = "(\\w+): capacity ((-)?\\d+), durability ((-)?\\d+), flavor ((-)?\\d+), texture ((-)?\\d+), calories ((-)?\\d+)";

        static void Main(string[] args)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            using (var fileStream = File.OpenRead("input.txt"))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        Match match = Regex.Match(line, pattern);
                        string name = match.Groups[1].Value;
                        int capacity = int.Parse(match.Groups[2].Value);
                        int durability = int.Parse(match.Groups[4].Value);
                        int flavor = int.Parse(match.Groups[6].Value);
                        int texture = int.Parse(match.Groups[8].Value);
                        int calories = int.Parse(match.Groups[10].Value);
                        ingredients.Add(new Ingredient() { Name = name, Capacity = capacity, Durability = durability, Flavor = flavor, Texture = texture, Calories = calories });
                    }
                }
            }
            

            List<Recipie> recipies = new List<Recipie>();

            int counter = 0;
            int max = (int)Math.Pow(100,ingredients.Count);
            while(counter < max)
            {
                int[] quantities = new int[ingredients.Count];
                int target = counter;
                int sum = 0;
                for (int i = 0; i < ingredients.Count; i++)
                {
                    int single = (target % 100);
                    quantities[i] = single;
                    sum += single;
                    target = (int)(target / 100);
                }
                if (sum == 100)
                {
                    var recipie = new Recipie() { Quantities = new int[ingredients.Count], Capacity = 0, Durability = 0, Flavor = 0, Texture = 0, Calories = 0, Score = 0 };
                    for (int i = 0; i < ingredients.Count; i++)
                    {
                        var ingredient = ingredients[i];
                        recipie.Quantities[i] = quantities[i];
                        recipie.Capacity += ingredient.Capacity * quantities[i];
                        recipie.Durability += ingredient.Durability * quantities[i];
                        recipie.Flavor += ingredient.Flavor * quantities[i];
                        recipie.Texture += ingredient.Texture * quantities[i];
                        recipie.Calories += ingredient.Calories * quantities[i];
                    }
                    recipie.Score = Math.Max(recipie.Capacity, 0) * Math.Max(recipie.Durability, 0) * Math.Max(recipie.Flavor, 0) * Math.Max(recipie.Texture, 0);
                    recipies.Add(recipie);
                }
                counter++;
            }

            var best = recipies.OrderByDescending(r => r.Score).First();
            Console.Write("The best overall recipie had these quantities [");
            foreach (var quantity in best.Quantities)
            {
                Console.Write(quantity + " ");
            }
            Console.WriteLine("] and a score of {0}", best.Score);
            var bestCalories = recipies.Where(r => r.Calories == 500).OrderByDescending(r => r.Score).First();
            Console.Write("The best recipie with exactly 500 calories had these quantities [");
            foreach (var quantity in bestCalories.Quantities)
            {
                Console.Write(quantity + " ");
            }
            Console.WriteLine("] and a score of {0}", bestCalories.Score);
            Console.ReadLine();
        }
    }
}
