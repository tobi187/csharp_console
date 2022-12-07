using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_console.entwicklerheld
{
    internal class Aoc22D7
    {
        public static readonly int highest = 100000;
        public void ReadFile()
        {
            var file = File.ReadAllLines("entwicklerheld/aoc22d7.txt");
            var baseDir = new Dir(name: "/", parent:"/");
            var flattenedList = new List<Dir>();
            var cDir = baseDir;
            foreach (var line in file)
            {
                if (line.StartsWith("$ cd")) 
                {
                    if (line.Substring(5, 1) == "/") cDir = baseDir;
                    if (line.Substring(5, 1) == ".")
                    {
                        var temp = FindDir(cDir!.parent, baseDir);
                        if (temp == null)
                            throw new Exception();
                        cDir = temp;
                    
                    }
                    else
                    {
                        var p = line.Split()[^1];
                        if (FindDir(p, baseDir) == null)
                        {
                            var d = new Dir(name: p, parent: cDir!.name);
                            cDir.dirs.Add(d);
                            cDir = d;
                            flattenedList.Add(d);
                        }
                    }
                }
                if (char.IsNumber(line[0]))
                {
                    var size = int.Parse(line.Split().First());
                    cDir!.files.Add(size);
                }
            }
            PrintTree(baseDir, 0);
            while (flattenedList.Any(x => x.size == -1))
            {
                foreach (var d in flattenedList)
                {
                    if (!d.dirs.Any()) 
                        d.size = d.files.Sum();
                    if (d.dirs.All(x => x.size != -1))
                        d.size = d.dirs.Sum(x => x.size) + d.files.Sum();
                }
            }
            Console.WriteLine(flattenedList.Where(x => x.size <= highest).Sum(x => x.size));
        }
        
        public void PrintFlattenedTree(Dir baseDir, int offset) {
            Console.WriteLine(new string(' ', offset) + "-" + " (dir) " + baseDir.name);
            foreach (var line in baseDir.files)
                Console.WriteLine(new string(' ', offset+4)+ " (file) " + line.ToString());
            foreach (var d in baseDir.dirs)
                PrintTree(d, offset + 4);

        }
        public void PrintTree(Dir baseDir, int offset)
        {
            Console.WriteLine(new string(' ', offset) + "-" + " (dir) " + baseDir.name);
            foreach (var line in baseDir.files)
                Console.WriteLine(new string(' ', offset+4)+ " (file) " + line.ToString());
            foreach (var d in baseDir.dirs)
                PrintTree(d, offset + 4);
        }

        public Dir? FindDir(string name,Dir baseDir)
        {
            if (name == baseDir.name)
                return baseDir;
            
            foreach (var n in baseDir.dirs)
            {
                var res = FindDir(name, n);
                if (res != null ) return res;
            }
            return null;
        }

    }

    internal class Dir
    {
        public string name;
        public string parent;
        public List<int> files = new();
        public List<Dir> dirs = new();
        public int size { get; set; } = -1;

        public static readonly int highest = 100000;
        public Dir(string name, string parent)
        {
            this.name = name;
            this.parent = parent;
        }
        

        public int GetSize() => dirs.Select(x=>x.GetSize()).Sum() + files.Sum();
        public void SetSize()
        {
            
        }
    }
    internal class FileSize
    {
        public string Name { get; set; }
        public int Size { get; set; }
    }
}
