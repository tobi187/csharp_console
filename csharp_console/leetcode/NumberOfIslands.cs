using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_console.leetcode
{
    internal class NumberOfIslands
    {
        char[][] Islands = new char[][] {
          new char[] {'1','1','0','0','0'},
          new char[] {'1','1','0','0','0'},
          new char[] {'0','0','1','0','0'},
          new char[] {'0','0','0','1','1'}
        };

        public (int x, int y) currentPos = (x: 0, y: 0);
        public List<(int x, int y)> visitedNodes = new();
        public Stack<(int x, int y)> deque = new();
        
        public void DoBfs()
        {
            while (deque.Count > 0)
            {
                currentPos = deque.Pop();
                if (visitedNodes.Contains(currentPos))
                    continue;
                visitedNodes.Add(currentPos);

            }
        }

        public void DetermineNextValue()
        {
            
        }
    }
}
