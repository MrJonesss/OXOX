using System;
using Domain.Models;
using Domain.Exceptions;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var board = new Board();
            var human = new Player("Alice", 'X');
            var ai = new MinimaxAiPlayer("Bot", 'O');

            PrintBoard(board);

            while (!board.IsFull() && board.CheckWinnerPublic() == null)
            {
                // Mens zet

                Console.WriteLine("Jouw zet (rij,kólom): ");
                var input = Console.ReadLine()?.Split(',');
                int row = int.Parse(input[0]);
                int col = int.Parse(input[1]);

                try
                {
                    board.PlaceMark(row, col, human.Symbol);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue; // vraag opnieuw
                }

                PrintBoard(board);

                if (board.IsGameOver) break;

                // AI zet
                var (aiRow, aiCol) = ai.GetMove(board);
                board.PlaceMark(aiRow, aiCol, ai.Symbol);
                Console.WriteLine($"AI zet: {aiRow},{aiCol}");
                PrintBoard(board);
            }

            // Resultaat
            var winner = board.CheckWinnerPublic();
            if (winner == null) Console.WriteLine("Gelijkspel!");
            else Console.WriteLine($"Winnaar: {winner}");

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
}
