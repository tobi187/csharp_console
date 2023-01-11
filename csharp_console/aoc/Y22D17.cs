using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_console.aoc
{
    internal class Y22D17
    {
        public List<string> chamber = new List<string>();
        
    }

    public class Point
    {
        public int x; 
        public int y;
        public string el { get; set; } = ".";
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
