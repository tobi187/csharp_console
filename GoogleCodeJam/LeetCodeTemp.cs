using System;
using System.Collections.Generic;
using System.Linq;

namespace GoogleCodeJam
{
    public class Solution
    {
        public int MinStoneSum(int[] piles, int k)
        {
            Array.Sort(piles, delegate (int a, int b) { return b - a; });
            piles[0] -= piles[0] / 2;
            if (piles.Length < 2)
            {
                for (var i = 1; i < k; i++)
                {
                    piles[0] -= piles[0] / 2;
                    if (piles[0] == 1) return 1;
                }
                return piles[0];
            }
            for (var i = 1; i < k; i++)
            {
                int li = -1;
                int le = -1;
                for (var j = 0; j < Math.Min(i + 1, piles.Length); j++)
                {
                    if (piles[j] > le)
                    {
                        le = piles[j];
                        li = j;
                    }
                }
                piles[li] -= piles[li] / 2;
            }
            return piles.Sum();
        }
    }
}