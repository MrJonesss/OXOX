using Domain.Exceptions;
using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class Board
    {
        public char[,] Cells { get; private set; } = new char[3, 3];
        public bool IsGameOver { get; private set; } = false;
        public char Winner { get; private set; } = '\0';

        public int Size => 3; // handig voor loops in AI

        public void PlaceMark(int row, int col, char mark)
        {
            if (IsGameOver)
                throw new GameOverException("Game is already over.");

            if (row < 0 || row > 2 || col < 0 || col > 2)
                throw new InvalidMoveException("Move is out of bounds.");

            if (Cells[row, col] != '\0')
                throw new InvalidMoveException("Cell is already occupied.");

            Cells[row, col] = mark;

            Winner = CheckWinner();
            if (Winner != '\0' || IsFull())
                IsGameOver = true;
        }

        public bool IsFull()
        {
            foreach (var cell in Cells)
                if (cell == '\0') return false;
            return true;
        }

        private char CheckWinner()
        {
            for (int i = 0; i < 3; i++)
            {
                // Rows
                if (Cells[i, 0] != '\0' && Cells[i, 0] == Cells[i, 1] && Cells[i, 1] == Cells[i, 2])
                    return Cells[i, 0];

                // Columns
                if (Cells[0, i] != '\0' && Cells[0, i] == Cells[1, i] && Cells[1, i] == Cells[2, i])
                    return Cells[0, i];
            }

            // Diagonals
            if (Cells[0, 0] != '\0' && Cells[0, 0] == Cells[1, 1] && Cells[1, 1] == Cells[2, 2])
                return Cells[0, 0];

            if (Cells[0, 2] != '\0' && Cells[0, 2] == Cells[1, 1] && Cells[1, 1] == Cells[2, 0])
                return Cells[0, 2];

            return '\0';
        }


        public bool IsCellEmpty(int row, int col)
        {
            return Cells[row, col] == '\0';
        }

        public void PlaceSymbol(int row, int col, char symbol)
        {
            Cells[row, col] = symbol;
        }

        public void RemoveSymbol(int row, int col)
        {
            Cells[row, col] = '\0';
        }

        public IEnumerable<(int row, int col)> GetEmptyCells()
        {
            for (int r = 0; r < Size; r++)
                for (int c = 0; c < Size; c++)
                    if (Cells[r, c] == '\0')
                        yield return (r, c);
        }

        public char? CheckWinnerPublic()
        {
            var w = CheckWinner();
            return w == '\0' ? (char?)null : w;
        }
    }
}
