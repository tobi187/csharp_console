using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_console
{
    internal class leetcode
    {
        public int DivisorSubstrings(int num, int k)
        {
            var divCount = 0;
            var n = num.ToString();
            for (int i = 0; i < n.Length - k + 1; i++)
            {
                var e = n.Substring(i, k);
                if (int.Parse(e) == 0)
                    continue;
                if (num % int.Parse(e) == 0) 
                    divCount++;
            }
            return divCount;
        }

        public int WaysToSplitArray(int[] nums)
        {
            long count = 0;
            long fst = 0;
            long snd = nums.Sum();
            for (var i = 0; i < nums.Length - 1; i++)
            {
                //var leftPart = nums.Where((_, index) => index <= i).Sum();
                //var rightPart = nums.Where((_, index) => index > i).Sum();
                fst += (long)nums[i];
                snd -= nums[i]; 
                if (fst > snd)
                    count++;
            }
            return (int)count;
        }

        public int MaximumWhiteTiles(int[][] tiles, int carpetLen)
        {
            var ot = tiles.OrderBy(x => x[0]).ToArray();
            var op = new List<int>();
            var tileNums = ot.SelectMany(x => x).ToArray();
            for (var i = 0; i < ot.Last()[1]; i++)
            {
                var l = 0;
                for (var j = i; j < carpetLen; j++)
                {
                    if (tileNums.Contains(l))
                        l += 1;
                }
                op.Add(l);
            }
            return op.Max();
        }
    }
    
}
