using Domain.Models;
using Xunit;

namespace Domain.Tests
{
    public class MinimaxAiPlayerTests
    {
        [Fact]
        public void GetMove_ShouldReturnValidEmptyCell_OnEmptyBoard()
        {
            var board = new Board();
            var ai = new MinimaxAiPlayer("Bot", 'X');

            var (row, col) = ai.GetMove(board);

            Assert.InRange(row, 0, 2);
            Assert.InRange(col, 0, 2);
            Assert.True(board.IsCellEmpty(row, col));
        }

        [Fact]
        public void GetMove_ShouldReturnMinusOne_WhenBoardFull()
        {
            var board = new Board();
            var ai = new MinimaxAiPlayer("Bot", 'X');

            // vul bord volledig
            char mark = 'X';
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    board.PlaceSymbol(r, c, mark);

            var move = ai.GetMove(board);
            Assert.Equal((-1, -1), move);
        }

        [Fact]
        public void GetMove_ShouldReturnMinusOne_WhenWinnerExists()
        {
            var board = new Board();
            var ai = new MinimaxAiPlayer("Bot", 'X');

            board.PlaceMark(0, 0, 'X');
            board.PlaceMark(0, 1, 'X');
            board.PlaceMark(0, 2, 'X'); // X wint

            var move = ai.GetMove(board);
            Assert.Equal((-1, -1), move);
        }

        [Fact]
        public void GetMove_ShouldWinIfPossible()
        {
            var board = new Board();
            var ai = new MinimaxAiPlayer("Bot", 'X');

            // AI kan winnen met (0,2)
            board.PlaceMark(0, 0, 'X');
            board.PlaceMark(0, 1, 'X');

            var move = ai.GetMove(board);

            Assert.Equal((0, 2), move);
        }

        [Fact]
        public void GetMove_ShouldBlockOpponentWin()
        {
            var board = new Board();
            var ai = new MinimaxAiPlayer("Bot", 'O');

            // Mens dreigt te winnen met (0,2)
            board.PlaceMark(0, 0, 'X');
            board.PlaceMark(0, 1, 'X');

            var move = ai.GetMove(board);

            Assert.Equal((0, 2), move); // AI moet blokkeren
        }

        [Fact]
        public void Ai_ShouldNeverLoseAgainstSimplePlayer()
        {
            var board = new Board();
            var ai = new MinimaxAiPlayer("Bot", 'O');
            char humanSymbol = 'X';

            // Simuleer menselijke zetten: altijd eerste lege cel
            while (!board.IsFull() && board.CheckWinnerPublic() == null)
            {
                // Mens zet: kies eerste lege cel
                var humanMove = board.GetEmptyCells().First();
                board.PlaceMark(humanMove.row, humanMove.col, humanSymbol);

                if (board.CheckWinnerPublic() != null || board.IsFull())
                    break;

                // AI zet
                var aiMove = ai.GetMove(board);
                if (aiMove.row != -1 && aiMove.col != -1)
                {
                    board.PlaceMark(aiMove.row, aiMove.col, ai.Symbol);
                }
            }

            // Controleer dat AI niet verloren heeft
            var winner = board.CheckWinnerPublic();

            // Winnaar mag null (gelijkspel) of AI zijn
            Assert.True(winner == null || winner == ai.Symbol, "AI verloor het spel!");
        }

        [Fact]
        public void Ai_ShouldPlayFullGame_WithoutExceptions()
        {
            var board = new Board();
            var ai = new MinimaxAiPlayer("Bot", 'X');
            char humanSymbol = 'O';

            var exceptionThrown = false;

            try
            {
                while (!board.IsFull() && board.CheckWinnerPublic() == null)
                {
                    // Mens zet: kies eerste lege cel
                    var humanMove = board.GetEmptyCells().First();
                    board.PlaceMark(humanMove.row, humanMove.col, humanSymbol);

                    if (board.CheckWinnerPublic() != null || board.IsFull())
                        break;

                    // AI zet
                    var aiMove = ai.GetMove(board);
                    if (aiMove.row != -1 && aiMove.col != -1)
                    {
                        board.PlaceMark(aiMove.row, aiMove.col, ai.Symbol);
                    }
                }
            }
            catch
            {
                exceptionThrown = true;
            }

            Assert.False(exceptionThrown, "Er werd een exception gegooid tijdens het spel!");
            Assert.True(board.IsFull() || board.CheckWinnerPublic() != null, "Het spel moet correct eindigen.");
        }
    }
}
