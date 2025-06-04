using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GridHackConsoleApp.Models;
using GridHackConsoleApp.Utils;

namespace GridHackConsoleApp.Services
{
    public class GameSession
    {
        public List<Node> Nodes { get; private set; } = new List<Node>();
        public List<Edge> Edges { get; private set; } = new List<Edge>();
        public List<GameRecord> History { get; private set; } = new List<GameRecord>();
        public string Rank { get; private set; } = "";

        private Stopwatch _stopwatch = new Stopwatch();

        public GameSession()
        {
            InitializeGame();
        }

        private void InitializeGame()
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

            History = StorageUtils.LoadHistory() ?? new List<GameRecord>();
            Rank = "";
            _stopwatch.Restart();
        }

        public void PrintNetworkStatus()
        {
            // Cabeçalho com borda
            Console.WriteLine("═══════════════════════════════════");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("     STATUS DA REDE ELÉTRICA      ");
            Console.ResetColor();
            Console.WriteLine("═══════════════════════════════════\n");

            // Exibir cada nó com cor
            foreach (var node in Nodes)
            {
                if (node.Isolated)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"Nó {node.Id}");
                    Console.ResetColor();
                    Console.WriteLine(" : Isolado");
                }
                else if (node.Failed)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"Nó {node.Id}");
                    Console.ResetColor();
                    Console.WriteLine(" : Falha (pulsante)");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"Nó {node.Id}");
                    Console.ResetColor();
                    Console.WriteLine(" : OK");
                }
            }

            // Verifica conexão e exibe
            bool connected = IsNetworkConnected();
            Console.WriteLine();
            Console.ForegroundColor = connected ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"═ Rede conectada (ignorando isolados): {(connected ? "SIM" : "NÃO")} ═");
            Console.ResetColor();
        }

        public bool ToggleIsolateNode(string nodeId)
        {
            var node = Nodes.FirstOrDefault(n => n.Id == nodeId);
            if (node == null || !node.Failed)
                return false;

            node.Isolated = !node.Isolated;
            return true;
        }

        public void FinalizeGame()
        {
            Console.WriteLine("═══════════════════════════════════");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("      FINALIZANDO ATIVIDADE       ");
            Console.ResetColor();
            Console.WriteLine("═══════════════════════════════════\n");

            if (!CanFinish())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("► Você precisa isolar todos os nós com falha e garantir que a rede esteja conectada.");
                Console.ResetColor();
                return;
            }

            _stopwatch.Stop();

            int points = 0;
            foreach (var node in Nodes)
            {
                if (node.Failed && node.Isolated) points += 20;
                else if (!node.Failed && node.Isolated) points -= 10;
            }

            Rank = CalculateRank(points);

            var record = new GameRecord(
                DateTime.Now,
                Nodes.Count(n => n.Failed && n.Isolated),
                (int)_stopwatch.Elapsed.TotalSeconds,
                Rank
            );

            History.Add(record);
            StorageUtils.SaveHistory(History);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"► Atividade finalizada!");
            Console.ResetColor();
            Console.WriteLine($"  Pontos obtidos: {points}");
            Console.WriteLine($"  Rank atual : {Rank}");
            Console.WriteLine($"  Tempo total: {(int)_stopwatch.Elapsed.TotalSeconds} segundos");
        }

        private bool CanFinish()
        {
            bool allFailedIsolated = Nodes.Where(n => n.Failed).All(n => n.Isolated);
            return allFailedIsolated && IsNetworkConnected();
        }

        private bool IsNetworkConnected()
        {
            var graph = new Dictionary<string, List<string>>();
            foreach (var node in Nodes.Where(n => !n.Isolated))
                graph[node.Id] = new List<string>();

            foreach (var edge in Edges)
            {
                if (graph.ContainsKey(edge.From) && graph.ContainsKey(edge.To))
                {
                    graph[edge.From].Add(edge.To);
                    graph[edge.To].Add(edge.From);
                }
            }

            if (graph.Count == 0) return false;

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

        private string CalculateRank(int points)
        {
            if (points >= 60) return "Ouro";
            if (points >= 30) return "Prata";
            return "Bronze";
        }

        public void ShowStatistics()
        {
            Console.WriteLine("═══════════════════════════════════");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("      HISTÓRICO DE ATIVIDADES      ");
            Console.ResetColor();
            Console.WriteLine("═══════════════════════════════════\n");

            if (History.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Nenhuma atividade registrada.");
                Console.ResetColor();
                return;
            }

            // Cabeçalho da tabela
            Console.WriteLine(" Data e Hora          | Falhas Ins. | Tempo(s) | Rank");
            Console.WriteLine("─────────────────────────────────────────────────────────");

            // Linhas do histórico
            foreach (var rec in History)
            {
                var dateStr = rec.Date.ToString("dd/MM/yyyy HH:mm");
                Console.WriteLine($"{dateStr} |      {rec.FailedIsolated,2}      |    {rec.TimeSeconds,3}   | {rec.Rank}");
            }
        }

        public void ClearHistory()
        {
            History.Clear();
            StorageUtils.ClearHistory();
        }
    }
}
