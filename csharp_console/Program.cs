using csharp_console;
using csharp_console.shortestPath;
using csharp_console.leetcoding;
using csharp_console.entwicklerheld;

var g = new int[10, 10]
                     {{1, 0, 0, 0, 0, 1, 1, 0, 0, 0},
                      {1, 0, 1, 0, 0, 0, 0, 0, 1, 0},
                      {1, 0, 1, 0, 1, 1, 1, 0, 1, 0},
                      {1, 0, 0, 0, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 0, 1, 1, 1, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 1, 0},
                      {0, 0, 0, 1, 0, 0, 0, 0, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 1, 0, 0},
                      {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}};

var e = BattleShip.ValidateBattlefield(g);

Console.WriteLine(e);

//var n2 = new TreeNode(val: 2);
//var n3 = new TreeNode(val: 3);
//var n1 = new TreeNode(val: 1, left: n2, right: n3);

//var bs = new Solution();
//var resul = bs.LargestValues(n1);

//foreach(var ddd in resul) Console.WriteLine(ddd);
//return;

//var f = File.ReadAllLines("shortestPath/cF.txt");

//var data = Enumerable.Range(1, 5).Select(x => x.ToString());

//var adjList = new Dictionary<string, List<Edges>>();

//foreach (var ch in data)
//{
//    var edges = new List<Edges>();
//    foreach (var line in f)
//    {
//        if (line.StartsWith(ch))
//        {
//            var dw = line.Split();
//            var ed = new Edges(dw[1], int.Parse(dw[2]));
//            edges.Add(ed);
//        }
//    }
//    adjList.Add(ch, edges);
//}

//var d = new CodeforcesDeikstra(adjList, "1", "5");

//Console.WriteLine(d.GameLoop());

//return;

//var c = File.ReadAllLines("shortestPath/citys.txt");

//var e = File.ReadAllLines("shortestPath/weights.txt");

//// Dict<Node, List<Node>> -> 

//ValueTuple<Node, List<Node>> GetAdjacency(string n)
//{
//    var neighbours = new List<Node>();
//    for (var i = 0; i < e.Length; i += 3)
//    {
//        if (n == e[i])
//        {
//            var node = new Node(e[i + 1], int.Parse(e[i + 2]));
//            neighbours.Add(node);
//        }
//    }
    
//    return (new Node(n), neighbours);
//}

//var adjacencyList = c.Select(x => GetAdjacency(x)).ToDictionary(x => x.Item1, x => x.Item2);

//var b = c.Select(x => new Node(x));

//var dijkstra = new Deikstra(adjacencyList, "ATL", "LAX");

//var r = dijkstra.FindShortestPath();

//Console.WriteLine(r);

//return;

//var bj = new BlackJack(new List<string>() { "Tobi" }, "Dealer");
//bj.PlayRound();


//var l = new Leetcode();

//var cd = l.MaximumWhiteTiles(new int[][] { new int[] { 1, 5 }, new int[] { 10, 11 }, new int[] { 12, 18 }, new int[] { 20, 25 }, new int[] { 30, 32 } }, 10);
//Console.WriteLine(c);


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

