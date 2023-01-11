using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_console.aoc
{
    internal class Y22D18
    {
        public List<Point22> point22s = new List<Point22>();
        public Dictionary<(int, int, int), List<(int, int, int)>> openSides = new Dictionary<(int, int, int), List<(int,int,int)>>();
        public void ReadFile()
        {
            foreach (var line in File.ReadAllLines("aoc/2215.txt"))
            {
                var ps = line.Split(",").Select(int.Parse).ToArray();
                var p = (ps[0], ps[1], ps[2]);
                openSides[p] = genPos(p.Item1, p.Item2, p.Item3);
            }

            foreach (var el in openSides.Keys)
            {
                FindNeighs(el);
            }

            var res = openSides.Values.Sum(x => x.Count);
            Console.WriteLine(res);
        }

        public List<(int, int, int)> genPos(int x, int y, int z) => 
            new List<(int, int, int)> { (x + 1, y, z), (x - 1, y, z), (x, y + 1, z), (x, y - 1, z), (x, y, z + 1), (x, y, z - 1) }; 

        public void FindNeighs((int,int,int)p)
        {
            foreach (var el in genPos(p.Item1, p.Item2, p.Item3))
            {
                if (openSides.ContainsKey(el))
                {
                    openSides[p].Remove(el);
                }
            }
        }

        public class Point22
        {
            public int x;
            public int y;
            public int z;
            public int sides = 6;
            public Point22(int x, int y, int z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }
    }
}
