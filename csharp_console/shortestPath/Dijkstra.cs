//graph node edges ? linked adjacency list ?? 

namespace csharp_console.shortestPath
{
    public class Deikstra
    {
        public Dictionary<Node, List<Node>> adjacencyList { get; private set; }
        public Node startNode { get; private set; }
        public Node endNode { get; private set; }
        public HashSet<Node> visitedNodes = new HashSet<Node>();
        public PriorityQueue<Node, int> priorityQueue = new PriorityQueue<Node, int>();
        public Dictionary<Node, Node> prevNodes = new Dictionary<Node, Node>();
        public Dictionary<Node, int> totalCost = new Dictionary<Node, int>();
        public Node currentNode { get; set; }

        public Deikstra(Dictionary<Node, List<Node>> aL, string sN, string eN)
        {
            adjacencyList = aL;
            startNode = adjacencyList.First(x => x.Key.Name == sN).Key;
            currentNode = startNode;
            endNode = adjacencyList.First(x => x.Key.Name == eN).Key;
            foreach (var item in adjacencyList.Keys) totalCost.Add(item, int.MaxValue);
            totalCost[startNode] = 0;
            priorityQueue.Enqueue(startNode, 0);
        }

        public string FindShortestPath()
        {
            while(!currentNode.Equals(endNode) && priorityQueue.Count != 0)
            {
                CheckNeigbours();
            }
            if (!currentNode.Equals(endNode))
                return "There is no shortest Path";

            var weight = totalCost[endNode];

            var path = new List<string>();

            var cN = endNode;

            while (!cN.Equals(startNode))
            {
                path.Add(cN.Name);
                cN = prevNodes.First(x => x.Key.Name == cN.Name).Value;
            }

            path.Add(startNode.Name);

            path.Reverse();

            return $"Länge: {weight}; Pfad war: {string.Join(" -> ", path)}";
        }

        public void CheckNeigbours()
        {
            var pN = priorityQueue.Dequeue();
            currentNode = adjacencyList.First(x => x.Key.Name == pN.Name).Key;
            visitedNodes.Add(currentNode);
            var currWeight = totalCost[currentNode];

            foreach (var node in adjacencyList[currentNode])
            {
                if (visitedNodes.Contains(node)) 
                    continue;
                UpdateQueues(node, currWeight);                
            }
        }
        public void UpdateQueues(Node node, int weigth)
        {
            var newWeight = weigth + node.Weight;
            if (totalCost.ContainsKey(node))
            {
                if (newWeight < totalCost[node])
                {
                    totalCost[node] = newWeight;
                    prevNodes[node] = currentNode;
                    priorityQueue.Enqueue(node, newWeight);
                }

            } 
            else
            {
                totalCost.Add(node, newWeight);
                prevNodes.Add(node, currentNode);
                priorityQueue.Enqueue(node, newWeight);
            }
        }

    }

    public class Node
    {
        public string Name;
        public int Weight { get; set; }
        public Node(string name)
        {
            Name = name;
        }
        public Node(string name, int weight) 
        {
            Name = name;
            Weight = weight;
        }
    }

    public class Edge
    {
        public Node Origin;
        public Node Destination;
        public int Weight;
        public Edge(Node o, Node d, int w)
        {
            Origin = o;
            Destination = d;
            Weight = w;
        }
    }

}