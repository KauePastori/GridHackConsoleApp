using System.Collections.Generic;
using System.Linq;
using GridHackConsoleApp.Models;

namespace GridHackConsoleApp.Services
{
    public class Network
    {
        public List<Node> Nodes { get; private set; }
        public List<Edge> Edges { get; private set; }

        public Network()
        {
            Initialize();
        }

        private void Initialize()
        {
            Nodes = new List<Node>
            {
                new Node("1", true),
                new Node("2", false),
                new Node("3", true),
                new Node("4", false),
                new Node("5", false)
            };

            Edges = new List<Edge>
            {
                new Edge("1", "2"),
                new Edge("2", "3"),
                new Edge("2", "4"),
                new Edge("4", "5")
            };
        }

        public bool IsConnected()
        {
            var graph = Nodes.Where(n => !n.Isolated)
                             .ToDictionary(n => n.Id, n => new List<string>());

            foreach (var edge in Edges)
            {
                var fromNode = Nodes.FirstOrDefault(n => n.Id == edge.From);
                var toNode = Nodes.FirstOrDefault(n => n.Id == edge.To);

                if (fromNode != null && toNode != null
                    && !fromNode.Isolated && !toNode.Isolated)
                {
                    graph[edge.From].Add(edge.To);
                    graph[edge.To].Add(edge.From);
                }
            }

            if (!graph.Any())
                return false;

            var visited = new HashSet<string>();
            var queue = new Queue<string>();
            queue.Enqueue(graph.Keys.First());

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                visited.Add(current);

                foreach (var neighbor in graph[current])
                {
                    if (!visited.Contains(neighbor))
                        queue.Enqueue(neighbor);
                }
            }

            return visited.Count == graph.Count;
        }
    }
}
