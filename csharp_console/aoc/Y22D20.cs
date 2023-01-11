namespace csharp_console.aoc
{
    public class Y22D20
    {
        public void Start()
        {
            var file = File.ReadAllLines("aoc/2215.txt");
            var hashes = new string[file.Length];
            var li = new List<(int e, string h)>();
            for (var i = 0; i < file.Length; i++)
            {
                var g = Guid.NewGuid().ToString();
                hashes[i] = g;
                li.Add((int.Parse(file[i]), g));
            }

            int[] res = new int[3];

            int cIndex = 0;

            for (var i = 1; i < 3001; i++)
            {
                Move(li, hashes[cIndex]);
                cIndex++;
                if (cIndex >= li.Count) cIndex = 0;
                if (i % 1000 == 0) AddRes(res, li, i);
                //System.Console.WriteLine(string.Join(", ", li.Select(x => x.e)));
            }
            System.Console.WriteLine(string.Join(", ", res));
        }

        public void AddRes(int[] res, List<(int e, string h)> d, int it)
        {
            for (var i = 0; i < d.Count; i++)
            {
                if (d[i].e == 0)
                {
                    if (i + 1 == d.Count)
                    {
                        res[(it - 100) / 1000] = d[0].e;
                    }
                    else
                    {
                        res[(it - 100) / 1000] = d[i + 1].e;
                    }
                    return;
                }
            }
            throw new Exception("0 not found");
        }

        public void Move(List<(int e, string h)> d, string hash)
        {
            var index = d.FindIndex(x => x.h == hash);
            var el = d[index];
            if (el.e < 0)
            { var nnhg = 1; }
            var newIndex = index;
            var c = el.e;
            int len = d.Count - 1;
            if (el.e < 0)
            { 
                while(c != 0)
                {
                    newIndex--;
                    if (newIndex < 1) newIndex = len;
                    c++;
                }
            }
            else if (el.e > 0)
            {
                //var tostart = el.e + index;
                //var rest = tostart % d.Count;
                //newIndex = rest;
                while (c != 0)
                {
                    newIndex++;
                    if (newIndex > len) newIndex = 1;
                    c--;
                }
            }
            else return;
            //var newIndex = index + el.e;
            //if (newIndex < 0) newIndex = d.Count - (newIndex*-1) % d.Count - 1;
            //if (newIndex >= d.Count) newIndex = newIndex % d.Count + 1;

            d.RemoveAt(index);
            if (newIndex >= d.Count || newIndex == 0) d.Add(el);
            else d.Insert(newIndex, el);
         }
    }
}
