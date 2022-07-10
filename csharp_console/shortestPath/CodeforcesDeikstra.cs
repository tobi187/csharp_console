using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_console.shortestPath
{
    internal class Programm
    {
        static void Main(string[] args)
        {

        }
    }

    internal class CodeforcesDeikstra
    {
        public Dictionary<string, List<Edges>> adjacencyList { get; set; }
        public string startNode;
        public string endNode;
        public HashSet<string> visitedNodes;
        public Dictionary<string, int> totalCost = new Dictionary<string, int>();
        public Dictionary<string, string> prevNodes = new Dictionary<string, string>();
        public PriorityQueue<string, int> priorityQueue = new PriorityQueue<string, int>();
        public string currentNode { get; set; }

        public CodeforcesDeikstra(Dictionary<string, List<Edges>> adjacencyList, string startNode, string endNode)
        {
            this.adjacencyList = adjacencyList;
            this.startNode = startNode;
            this.endNode = endNode;
            visitedNodes = new HashSet<string>() { startNode };
            foreach (var n in adjacencyList) totalCost.Add(n.Key, int.MaxValue);
            priorityQueue.Enqueue(startNode, 0);
            totalCost[startNode] = 0;
            currentNode = startNode;
        }

        public string GameLoop()
        {
            while (currentNode != endNode && priorityQueue.Count != 0)
            {
                CheckNeighbours();
            }
            if (currentNode != endNode)
                return "No Path found";

            var weight = totalCost[endNode];

            var path = new List<string>();

            var cN = endNode;

            while (!cN.Equals(startNode))
            {
                path.Add(cN);
                cN = prevNodes[cN];
            }

            path.Add(startNode);

            path.Reverse();

            return $"Länge: {weight}; Pfad war: {string.Join(" -> ", path)}";
        }

        public void CheckNeighbours()
        {
            currentNode = priorityQueue.Dequeue();
            visitedNodes.Add(currentNode);
            var currWeight = totalCost[currentNode];

            foreach (var node in adjacencyList[currentNode])
            {
                if (visitedNodes.Contains(node.Name))
                    continue;
                UpdateQueue(node, currWeight);
            }
        }

        public void UpdateQueue(Edges node, int weight)
        {
            var newWeight = weight + node.Weight;
            if (totalCost.ContainsKey(node.Name))
            {
                if (newWeight < totalCost[node.Name])
                {
                    totalCost[node.Name] = newWeight;
                    priorityQueue.Enqueue(node.Name, newWeight);
                    prevNodes[node.Name] = currentNode;
                }
            } else
            {
                totalCost.Add(node.Name, newWeight);
                prevNodes.Add(node.Name, currentNode);
                priorityQueue.Enqueue(node.Name, newWeight);
            }
        }
    }

    public class Edges
    {
        public string Name;
        public int Weight;

        public Edges(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }
    }
}
