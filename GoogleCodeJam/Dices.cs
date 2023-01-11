using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleCodeJam
{
    internal class Dices
    {
        public static void Main(string[] args)
        {
            var saf = new Solution();
            saf.MinStoneSum(new int[] { 1391, 5916 }, 3);
            return;

            int t = int.Parse(Console.ReadLine());
            var res = "";
            for (var tt = 0; tt < t; tt++)
            {
                res += $"Case #{tt + 1}: ";
                var w = int.Parse(Console.ReadLine());
                var s = Console.ReadLine().Split().Select(int.Parse).ToArray();
                Array.Sort(s);
                var r = 0;
                for (var i = 0; i < w; i++)
                    if (s[i] > i) r++;
                res += $"{r}\n";
            }
            Console.WriteLine(res);
        }
    }
}
