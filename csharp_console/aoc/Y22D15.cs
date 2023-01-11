using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace csharp_console.aoc
{
    internal class Y22D15
    {
        public int importantY = 20;
        public void MakeStuff()
        {
            var f = File.ReadAllLines("aoc/2215.txt");
            var lines = new List<Beacon>();

            foreach (var line in f)
            {
                var l = line.Split()
                    .Where(x => x.Contains("="))
                    .Select(x => x.Split("=")[1].TakeWhile(x => char.IsDigit(x) || x == '-'))
                    .Select(x => string.Concat(x))
                    .Select(int.Parse)
                    .ToArray();

                lines.Add(GetManhatten(l[0], l[1], l[2], l[3]));
            }

            for (var y = 0; y < 4_000_000; y++)
            {
                for (var x = 0; x < 4_000_000; x++)
                {
                    if (lines.All(b => !b.IsInRange(x, y)))
                    {
                        Console.WriteLine($"X: {x}; Y: {y}");
                        Console.WriteLine(x * 4000000 + y);
                        return;
                    }
                }
            }

            //CalcResult(lines);
        }

        public Beacon GetManhatten(int sx, int sy, int bx, int by)
        {
            var manhatten = Math.Abs(sx - bx) + Math.Abs(sy - by);
            return new Beacon(sx, sy, manhatten);
        }

        public void CalcResult(List<(int, int)> vals)
        {
            var res = vals
                .Select(x => Enumerable.Range(x.Item1, x.Item2 - x.Item1))
                .SelectMany(x => x)
                .Distinct()
                .Count();

            Console.WriteLine(res);
        }

        public (int, int)? GetBeacons(int sx, int sy, int bx, int by)
        {
            var yDiff = Math.Abs(sy - importantY);
            var manhatten = Math.Abs(sx - bx) + Math.Abs(sy - by);
            var xRange = manhatten - yDiff;
            if (xRange < 0) return null;
            return (sx - xRange, sx + xRange);
        }
    }
    public class Beacon
    {
        public int x;
        public int y;
        public int manhatten;

        public Beacon(int x, int y, int manhatten)
        {
            this.x = x;
            this.y = y;
            this.manhatten = manhatten;
        }

        public bool IsInRange(int px, int py)
            => Math.Abs(x - px) + Math.Abs(y - py) <= manhatten;
    }
} 
