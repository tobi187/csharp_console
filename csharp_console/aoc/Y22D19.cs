using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_console.aoc
{
    internal class Y22D19
    {     
        public void Start()
        {

        }
        public void Parse() 
        {
            var games = new List<Game>();
            var file = 
                File.ReadAllLines("aoc/2215.txt")
                .Select(x => x.Split(":").Last())
                .Select(x => x.Split("."));
                
            foreach (var line in file)
            {
                var game = new Game();
                foreach (var rs in line)
                {
                    var robot = new Robot();
                    
                }
            }
        }
    }

    public class Game
    {
        public Robot or { get; set; }
        public Robot cr { get; set; }
        public Robot obr { get; set; }
        public Robot gr { get; set; }
        public int orA { get; set; }
        public int crA { get; set; }
        public int obrA { get; set; }
        public int grA { get; set; }
        public int ore { get; set; }
        public int clay { get; set; }
        public int obsidian { get; set; }
        public int geode { get; set; }
    }

    public class Robot
    {
        public G rt { get; set; }
        public List<int> cost { get; set; } 
        public List<G> ct { get; set; }
    }
    public enum G
    {
        ore,
        clay,
        obsidian,
        geode
    }
}
