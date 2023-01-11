using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace csharp_console.aoc
{
    internal class Y22D22
    {
        public void Read()
        {
            var data = new Dictionary<string, Monkey>();
            var file = File.ReadAllLines("aoc/2215.txt");
            foreach (var l in file)
            {
                var m = new Monkey();
                var els = l.Split(':');
                
                m.name = els[0];
                var rest = els[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (long.TryParse(rest[0], out var n))
                {
                    m.value = n;
                }
                else
                {
                    m.operation = rest[1];
                    m.els = rest.Where(el => el.Length > 1).ToArray();
                }
                data.Add(m.name, m);
            }
            while (data["root"].value == null)
            {
                foreach (var e in data.Values)
                    if (e.value == null)
                        e.CalcNum(data);
            }

            var ms = new List<Monkey>();
            Backtrack("root", ms, "humn", data);

            var numToReach = data[data["root"].els.Where(x => x != ms[1].name).First()].value;

            for (int i = 1; i < ms.Count - 1; i++)
            {
                var parent = ms[i];
                // parent needs 150
                var immutKid = parent.els.Where(x => !ms.Select(x => x.name).Contains(x)).First();
                if (parent.operation == "/")
                {
                    if (parent.els[0] == immutKid)
                    {
                        numToReach = data[immutKid].value / numToReach; 
                    } else
                    {
                        numToReach = data[immutKid].value * numToReach;
                    } 
                } else if (parent.operation == "*")
                {
                    numToReach = numToReach / data[immutKid].value;
                }
                else if (parent.operation == "+")
                {
                    numToReach = numToReach - data[immutKid].value;
                }
                else if (parent.operation == "-")
                {
                    if (parent.els[0] == immutKid)
                    {
                        numToReach = -numToReach + data[immutKid].value;
                    }
                    else
                    {
                        numToReach = numToReach + data[immutKid].value;
                    }
                }
            }
            Console.WriteLine(numToReach);
        }
        public bool Backtrack(string curr, List<Monkey> m, string goal, Dictionary<string, Monkey> mon)
        {
            if (curr == null) return false;
            var cm = mon[curr];
            m.Add(cm);
            if (curr == goal) return true;
            if (Backtrack(cm.els[0], m, goal, mon) || Backtrack(cm.els[1], m, goal, mon))
                return true;

            m.RemoveAt(m.Count - 1);
            return false;
        } 

    }

    public class Monkey
    {
        public string name { get; set; }
        public long? value { get; set; } = null;
        public string[] els { get; set; } = new string[] { null, null };
        public string operation { get; set; }
        public void CalcNum(Dictionary<string, Monkey> ns)
        {
            var d = els
                .Select(x => ns[x])
                .Select(x => x.value)
                .ToArray();

            if (!d.Any(x => x == null))
                switch (operation)
                {
                    case "*": 
                        value = d[0] * d[1];
                        break;
                    case "/":
                        value = d[0] / d[1];
                        break;
                    case "+":
                        value = d.Sum();
                        break;
                    case "-":
                        value = d[0] - d[1];
                        break;
                }
        }
    }
}
