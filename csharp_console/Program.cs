//using System;
using csharp_console;

var bj = new BlackJack(new List<string>() { "Tobi"}, "Dealer");
bj.PlayRound();

return;

var l = new leetcode();

var c = l.MaximumWhiteTiles(new int[][] { new int[] { 1,5}, new int[] { 10,11},new int[] { 12,18}, new int[] { 20,25} ,new int[] { 30,32}}, 10);
Console.WriteLine(c);


//static bool IsHappy(int n)
//{
//    var res = n;
//    while (true)
//    {
//        var r = DoCalc(res);
//        if (r == 1)
//            return true;
//        if (r == res)
//            return false;
//        res = r;
//    }

//}
//static int DoCalc(int n)
//{
//    return
//        n.ToString()
//        .ToCharArray()
//        .Select(x => int.Parse(x.ToString()))
//        .Aggregate(0, (agg, x) => agg + (int)Math.Pow(x, 2));
//}

//static bool AreAlmostEqual(string s1, string s2)
//{
//    if (s1 == s2)
//        return true;
//    if (s1.Length != s2.Length)
//        return false;
//    var diffs = new List<int>();
//    for (var i = 0; i < s1.Length; i++)
//    {
//        if (s1[i] != s2[i])
//            diffs.Add(i);
//    }
//    if (diffs.Count() == 2)
//    {
//        var fc = s1[diffs[0]].ToString();  
//        var sc = s1[diffs[1]].ToString();
//        return s1.Remove(diffs[0], 1).Insert(diffs[0], sc).Remove(diffs[1], 1).Insert(diffs[1], fc) == s2;
//        }
//    return false;
//}

//static int[] whatever(int[] nums1, int[] nums2)
//{
//    var res = new List<int>();
//    var n2 = nums2.ToList();
//    for (var i = 0; i < nums1.Length; i++)
//    {
//        var ei = n2.IndexOf(nums1[i]);
//        var e = -1;
//        for (var j = 0; j < nums2.Length; j++)
//        {
//            if (nums1[i] < nums2[j])
//            {
//                e = nums2[j];
//                break;
//            }
//        }
//        res.Add(e);
//    }
//    return res.ToArray();
//}

//var z = whatever(new int[] {2, 4}, new int[] { 1, 2, 3, 4 });

//Console.WriteLine(string.Join(", ", z.Select(x => x.ToString())));

