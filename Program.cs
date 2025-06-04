using System;
using GridHackConsoleApp.Models;
using GridHackConsoleApp.Services;
using GridHackConsoleApp.Utils;

namespace GridHackConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Banner inicial em ASCII Art
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"  ____  ____  ____  _    _    _  _    _    _  ____    _  __    _    _  ____ ");
            Console.WriteLine(@" / ___)(  _ \(  _ \| \  ( \/\/ )( \/\/ )  / )(  _ \  / )(  )  ( \/\/ )(  _ \");
            Console.WriteLine(@" \___ \ )(_)( )   /)  (  \    /  )    (  (  O ))   /  \ \/\/ )  \  /  )   /");
            Console.WriteLine(@" (____/(____/(__\_)\_)_)  \/\/   )_/\/\_)  \__/(__\_)   \_)(_)\_)(_)(_(__\_)");
            Console.WriteLine();
            Console.ResetColor();

            var user = new User("user", "1234");
            var gameSession = new GameSession();

            Console.WriteLine("=== GridHack Console - Simulação de Rede Elétrica ===\n");

            // Loop de login
            while (!user.IsLoggedIn)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Usuário: ");
                Console.ResetColor();
                var username = Console.ReadLine() ?? "";

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Senha: ");
                Console.ResetColor();
                var password = Console.ReadLine() ?? "";

                if (user.Login(username, password))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nLogin realizado com sucesso!\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nUsuário ou senha inválidos, tente novamente.\n");
                    Console.ResetColor();
                }
            }

            bool exit = false;
            while (!exit)
            {
                // Menu principal com cores e bordas
                Console.WriteLine("══════════════════════════════════════════════════════");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(" MENU PRINCIPAL ");
                Console.ResetColor();
                Console.WriteLine("══════════════════════════════════════════════════════");
                Console.WriteLine(" 1. Mostrar status da rede e isolar/restaurar nós");
                Console.WriteLine(" 2. Finalizar atividade");
                Console.WriteLine(" 3. Ver estatísticas");
                Console.WriteLine(" 4. Limpar histórico de atividades");
                Console.WriteLine(" 5. Sair");
                Console.Write(" Escolha uma opção: ");

                var input = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    int option = int.Parse(input ?? "");
                    switch (option)
                    {
                        case 1:
                            // Agora mantém em loop até digitar "0"
                            ManageNetworkLoop(gameSession);
                            break;

                        case 2:
                            Console.Clear();
                            gameSession.FinalizeGame();
                            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                            Console.ReadKey(true);
                            Console.Clear();
                            break;

                        case 3:
                            Console.Clear();
                            gameSession.ShowStatistics();
                            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                            Console.ReadKey(true);
                            Console.Clear();
                            break;

                        case 4:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("Tem certeza que deseja apagar todo o histórico? (s/n): ");
                            Console.ResetColor();
                            var confirm = Console.ReadLine()?.ToLower();
                            if (confirm == "s")
                            {
                                gameSession.ClearHistory();
                                StorageUtils.ClearHistory();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("► Histórico apagado com sucesso!");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("► Operação cancelada.");
                                Console.ResetColor();
                            }
                            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
                            Console.ReadKey(true);
                            Console.Clear();
                            break;

                        case 5:
                            exit = true;
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Encerrando aplicação. Até logo!");
                            Console.ResetColor();
                            break;

                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Opção inválida. Digite um número entre 1 e 5.\n");
                            Console.ResetColor();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Entrada inválida. Por favor, digite um número válido.\n");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Erro inesperado: {ex.Message}\n");
                    Console.ResetColor();
                }
            }
        }

        private static void ManageNetworkLoop(GameSession gameSession)
        {
            bool stay = true;
            while (stay)
            {
                Console.Clear();
                // Exibe status da rede
                gameSession.PrintNetworkStatus();
                Console.WriteLine();

                // Prompt para isolar/restaurar ou sair
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Digite o ID do nó para isolar/restaurar (ou '0' para voltar ao menu): ");
                Console.ResetColor();
                string input = Console.ReadLine() ?? "";

                if (input == "0")
                {
                    stay = false; // volta ao menu principal
                }
                else
                {
                    bool success = gameSession.ToggleIsolateNode(input);
                    if (success)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n► Nó {input} isolado/restaurado com sucesso!");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n► ID inválido ou nó não pode ser isolado/restaurado.");
                        Console.ResetColor();
                    }

                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey(true);
                }
            }
            Console.Clear();
        }
    }
}
