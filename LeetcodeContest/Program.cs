




using System.Diagnostics.CodeAnalysis;
using System.Globalization;

var ra = MaxPower(new int[] { 4,4,4,4}, 9, 3);



long MaxPower(int[] stations, int r, int k)
{
    if (r > stations.Length) r = stations.Length-1;
    var ss = new int[stations.Length];
    var range = 0;
    var rr = 0;
    for (var i = 0; i < r*2+1 && i < stations.Length; i++)
    {
        range += stations[i];
        if (i <= r) rr += stations[i];    
    }
    
    ss[r] = range;
    for (var i = r+1; i < stations.Length-r; i++)
    {
        range -= stations[i - r - 1];
        range += stations[i + r];
        ss[i] = range;
    }

    ss[0] = rr;
    for (int i = 1; i < r; i++)
    {
        rr += stations[i + r < stations.Length ? i + r : stations.Length-1];
        ss[i] = rr;
    }
    rr = 0;
    var s = stations.Length - r * 2;
    var sss = s < 0 ? 0 : s;
    for (var  i = sss; i < stations.Length; i++)
        rr += stations[i];
    for (var i = sss; i < stations.Length; i++)
    {
        ss[i] = range;
        try
        {
            range -= stations[i - r];
        }catch { }
    }

    for (var i = 0; i < k; i++)
    {

    }

    foreach (var el in ss) Console.WriteLine(el);

    return (long)0;
}