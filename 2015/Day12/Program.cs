using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayTwelve
{
    class Program
    {
        static int Walk(JToken input)
        {
            int sum = 0;
            if (input.Type == JTokenType.Object || input.Type == JTokenType.Array)
            {
                int objectSum = 0;
                bool hasRed = false;
                foreach (JToken item in input)
                {
                    if (item.Type == JTokenType.Property)
                    {
                        var property = item as JProperty;
                        objectSum += Walk(property.Value);
                        if (property.Value.Type == JTokenType.String && property.Value.ToString() == "red")
                        {
                            hasRed = true;
                        }
                    }
                    else
                    {
                        objectSum += Walk(item);
                    }
                }
                if (!hasRed)
                {
                    sum += objectSum;
                }
            }
            else if(input.Type == JTokenType.Integer)
            {
                return input.Value<int>();
            }
            return sum;
        }
        static void Main(string[] args)
        {
            int sum = 0;
            using (var fileStream = File.OpenRead("input.txt"))
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    string jsonContent = streamReader.ReadToEnd();
                    JObject jsonObject = JsonConvert.DeserializeObject<JObject>(jsonContent);
                    sum = Walk(jsonObject);
                }
            }
            Console.WriteLine(sum.ToString());
            Console.ReadLine();
        }
    }
}
