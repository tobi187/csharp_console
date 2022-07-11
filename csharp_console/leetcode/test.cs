using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_console.leetcoding
{
    // https://leetcode.com/problems/find-largest-value-in-each-tree-row/
    public class TreeNode
      {
          public int val;
          public TreeNode? left;
          public TreeNode? right;
          public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
          {
            this.val = val;
            this.left = left;
            this.right = right;
          }
      }
    public class Solution
    {
        public IList<int> LargestValues(TreeNode root)
        {
            var whatever = new DoBfs(root);
            whatever.DoBFS();
            return whatever.GetLargestPerLevel();
        }
    }

    public class DoBfs
    {
        public TreeNode currentNode;
        public Queue<TreeNode> deque = new();
        public List<TreeNode> visitedNodes = new();
        public List<int[]> levels = new();
        public int nextLevelCounter = 0;
        public int[] currentLevelValues = new int[] {-1};

        public DoBfs(TreeNode rootNode)
        {
            deque.Enqueue(rootNode);
        }

        public void DoBFS()
        {
            while (deque.Count > 0)
            {
                currentNode = deque.Dequeue();
                var index = Array.IndexOf(currentLevelValues, -1);
                if (index == -1)
                {
                    levels.Add(currentLevelValues.ToArray());
                    currentLevelValues = new int[nextLevelCounter];
                    Array.Fill(currentLevelValues, -1);
                    nextLevelCounter = 0;
                }
                
                currentLevelValues[index] = currentNode.val;
                UpdateQueues();
            }

            if (currentLevelValues.Any(x => x != -1))
                levels.Add(currentLevelValues.Where(x => x != -1).ToArray());
        }

        public void UpdateQueues()
        {
            if (currentNode.left != null && !visitedNodes.Contains(currentNode.left))
            {
                deque.Enqueue(currentNode.left);
                visitedNodes.Add(currentNode.left);
                nextLevelCounter++;
            }
            if (currentNode.right != null && !visitedNodes.Contains(currentNode.right))
            {
                deque.Enqueue(currentNode.right);
                visitedNodes.Add(currentNode.right);
                nextLevelCounter++;
            }
        }

        public List<int> GetLargestPerLevel()
        {
            List<int> list = new List<int>();
            foreach (var element in levels)
            {
                list.Add(element.Max());
            }
            return list;
        }
    }
}
