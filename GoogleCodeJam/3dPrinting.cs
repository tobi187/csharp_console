using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace GoogleCodeJam
{
    public class Solution2
    {
        static void main(string[] args)
        {
            var lines = int.Parse(Console.ReadLine());
            var res = "";
            for (var t = 0; t < lines; t++)
            {
                res += $"Case #{t + 1}: ";
                var ps = new int[3 * 4];
                for (var l = 0; l < 3; l++) {
                    var _in = Console.ReadLine().Split();
                    for (var inner = 0; inner < 4; inner++)
                        ps[l * 4 + inner] = int.Parse(_in[inner]);
                }
                var mins = new int[4];
                Array.Fill(mins, int.MaxValue);
                for (var p = 0; p < 4; p++)
                    for (var _i = 0; _i < 3; _i++)
                        if (ps[p + _i * 4] < mins[p]) mins[p] = ps[p + _i * 4];

                if (mins.Sum() < Math.Pow(10, 6))
                {
                    res += "IMPOSSIBLE\n";
                    continue;
                }
                //Array.Sort(mins, delegate (int a, int b) { return b - a; });
                var s = 0;
                for (var i = 0; i < mins.Length; i++)
                {
                    if (s + mins[i] >= Math.Pow(10, 6))
                    {
                        res += string.Join(" ", mins.Take(i));
                        res += $" {Math.Pow(10, 6) - s}";
                        for (var e = i+1; e < mins.Length; e++)
                            res += " 0";
                        res += "\n";
                        break;
                    }
                    s += mins[i];
                }
            }
            Console.WriteLine(res);
        }
    }
}
