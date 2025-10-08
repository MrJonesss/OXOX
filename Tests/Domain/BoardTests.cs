using Domain.Exceptions;
using Domain.Models;
using System;
using System.Linq;
using Xunit;

namespace Tests.Domain
{
    public class BoardTests
    {
        [Fact]
        public void PlaceMark_ShouldPlaceMarkCorrectly()
        {
            var board = new Board();
            board.PlaceMark(0, 0, 'X');

            Assert.Equal('X', board.Cells[0, 0]);
            Assert.False(board.IsGameOver);
            Assert.Null(board.CheckWinnerPublic());
        }

        [Fact]
        public void PlaceMark_OutOfBounds_ShouldThrowException()
        {
            var board = new Board();

            Assert.Throws<InvalidMoveException>(() => board.PlaceMark(-1, 0, 'X'));
            Assert.Throws<InvalidMoveException>(() => board.PlaceMark(0, -1, 'X'));
            Assert.Throws<InvalidMoveException>(() => board.PlaceMark(3, 0, 'X'));
            Assert.Throws<InvalidMoveException>(() => board.PlaceMark(0, 3, 'X'));
        }

        [Fact]
        public void PlaceMark_CellAlreadyOccupied_ShouldThrowException()
        {
            var board = new Board();
            board.PlaceMark(0, 0, 'X');

            Assert.Throws<InvalidMoveException>(() => board.PlaceMark(0, 0, 'O'));
        }

        [Fact]
        public void PlaceMark_GameOver_ShouldThrowException()
        {
            var board = new Board();

            // Vul het bord met een winnaar
            board.PlaceMark(0, 0, 'X');
            board.PlaceMark(0, 1, 'X');
            board.PlaceMark(0, 2, 'X'); // rij vol, X wint

            Assert.True(board.IsGameOver);
            Assert.Equal('X', board.Winner);

            Assert.Throws<GameOverException>(() => board.PlaceMark(1, 0, 'O'));
        }

        [Theory]
        [InlineData('X')]
        [InlineData('O')]
        public void CheckWinner_Rows_ShouldReturnWinner(char symbol)
        {
            var board = new Board();
            for (int c = 0; c < 3; c++)
                board.PlaceSymbol(0, c, symbol);

            Assert.Equal(symbol, board.CheckWinnerPublic());
        }

        [Theory]
        [InlineData('X')]
        [InlineData('O')]
        public void CheckWinner_Columns_ShouldReturnWinner(char symbol)
        {
            var board = new Board();
            for (int r = 0; r < 3; r++)
                board.PlaceSymbol(r, 1, symbol);

            Assert.Equal(symbol, board.CheckWinnerPublic());
        }

        [Theory]
        [InlineData('X')]
        [InlineData('O')]
        public void CheckWinner_Diagonals_ShouldReturnWinner(char symbol)
        {
            var board1 = new Board();
            board1.PlaceSymbol(0, 0, symbol);
            board1.PlaceSymbol(1, 1, symbol);
            board1.PlaceSymbol(2, 2, symbol);
            Assert.Equal(symbol, board1.CheckWinnerPublic());

            var board2 = new Board();
            board2.PlaceSymbol(0, 2, symbol);
            board2.PlaceSymbol(1, 1, symbol);
            board2.PlaceSymbol(2, 0, symbol);
            Assert.Equal(symbol, board2.CheckWinnerPublic());
        }

        [Fact]
        public void IsFull_ShouldReturnTrue_WhenBoardFull()
        {
            var board = new Board();
            char mark = 'X';

            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    board.PlaceSymbol(r, c, mark);

            Assert.True(board.IsFull());
            Assert.Empty(board.GetEmptyCells());
        }

        [Fact]
        public void IsCellEmpty_ShouldReturnTrueOrFalseCorrectly()
        {
            var board = new Board();
            Assert.True(board.IsCellEmpty(0, 0));
            board.PlaceSymbol(0, 0, 'X');
            Assert.False(board.IsCellEmpty(0, 0));
        }

        [Fact]
        public void RemoveSymbol_ShouldEmptyCell()
        {
            var board = new Board();
            board.PlaceSymbol(1, 1, 'X');
            Assert.False(board.IsCellEmpty(1, 1));

            board.RemoveSymbol(1, 1);
            Assert.True(board.IsCellEmpty(1, 1));
        }

        [Fact]
        public void GetEmptyCells_ShouldReturnAllEmptyCells()
        {
            var board = new Board();
            board.PlaceSymbol(0, 0, 'X');
            board.PlaceSymbol(1, 1, 'O');

            var emptyCells = board.GetEmptyCells().ToList();
            Assert.Equal(7, emptyCells.Count);
            Assert.DoesNotContain((0, 0), emptyCells);
            Assert.DoesNotContain((1, 1), emptyCells);
        }
    }
}
