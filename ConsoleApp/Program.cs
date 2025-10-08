using Data.Repository;
using Domain.Interfaces;
using Domain.Interfaces.Data;
using Domain.Models;
using System;
using System.Linq;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welkom bij OXOX!");

            // Kies spelernaam
            Console.Write("Voer je naam in: ");
            string playerName = Console.ReadLine() ?? "Speler";

            // Initialiseer repository (Data-laag)
            IPlayerScoreRepository repo = new PlayerScoreRepository("scores.json");

            // Haal of maak PlayerScore
            var playerScore = repo.GetByName(playerName) ?? new PlayerScore(playerName);

            // Initialiseer spelers
            var human = new Player(playerName, 'X');
            var ai = new MinimaxAiPlayer("Bot", 'O');

            bool speelOpnieuw = true;

            while (speelOpnieuw)
            {
                var board = new Board();
                PrintBoard(board);

                while (!board.IsFull() && board.CheckWinnerPublic() == null)
                {
                    // Mens zet
                    Console.WriteLine("Jouw zet (rij,kólom): ");
                    var input = Console.ReadLine()?.Split(',');
                    if (input == null || input.Length != 2 || !int.TryParse(input[0], out int row) || !int.TryParse(input[1], out int col))
                    {
                        Console.WriteLine("Ongeldige invoer. Gebruik formaat: rij,col (0-2)");
                        continue;
                    }

                    try
                    {
                        board.PlaceMark(row, col, human.Symbol);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }

                    PrintBoard(board);

                    if (board.IsGameOver) break;

                    // AI zet
                    var (aiRow, aiCol) = ai.GetMove(board);
                    if (aiRow != -1 && aiCol != -1)
                    {
                        board.PlaceMark(aiRow, aiCol, ai.Symbol);
                        Console.WriteLine($"AI zet: {aiRow},{aiCol}");
                        PrintBoard(board);
                    }
                }

                // Resultaat
                var winner = board.CheckWinnerPublic();
                if (winner == null) Console.WriteLine("Gelijkspel!");
                else
                {
                    Console.WriteLine($"Winnaar: {winner}");
                    if (winner == human.Symbol)
                    {
                        playerScore.AddWin("unbeatable"); // AI moeilijkheid
                        repo.Save(playerScore);
                    }
                }

                // Toon huidige score
                Console.WriteLine($"Jouw score tegen Unbeatable AI: {playerScore.GetScore("unbeatable")}");

                // Vraag of opnieuw gespeeld wordt
                Console.Write("Nog een spel? (j/n): ");
                string opnieuw = Console.ReadLine() ?? "n";
                speelOpnieuw = opnieuw.Trim().ToLower() == "j";
            }

            Console.WriteLine("Bedankt voor het spelen!");
        }

        static void PrintBoard(Board board)
        {
            for (int r = 0; r < board.Size; r++)
            {
                for (int c = 0; c < board.Size; c++)
                {
                    char mark = board.Cells[r, c] == '\0' ? '-' : board.Cells[r, c];
                    Console.Write(mark + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
