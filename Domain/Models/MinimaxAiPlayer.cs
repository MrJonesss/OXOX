using System;
using Domain.Interfaces;

namespace Domain.Models
{
    public class MinimaxAiPlayer : AiPlayer
    {
        public MinimaxAiPlayer(string name, char symbol)
            : base(name, symbol)
        {
        }

        public override (int row, int col) GetMove(Board board)
        {
            // Als het bord vol is of er al een winnaar is, geen zet doen
            if (board.IsFull() || board.CheckWinnerPublic() != null)
                return (-1, -1); // speciale waarde, geen zet

            int bestScore = int.MinValue;
            (int row, int col) bestMove = (-1, -1);

            // Doorloop alle lege vakjes
            for (int r = 0; r < board.Size; r++)
            {
                for (int c = 0; c < board.Size; c++)
                {
                    if (board.IsCellEmpty(r, c))
                    {
                        board.PlaceSymbol(r, c, this.Symbol); // tijdelijke zet
                        int score = Minimax(board, depth: 0, isMaximizing: false, alpha: int.MinValue, beta: int.MaxValue);
                        board.RemoveSymbol(r, c); // reset
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = (r, c);
                        }
                    }
                }
            }

            return bestMove;
        }

        private int Minimax(Board board, int depth, bool isMaximizing, int alpha, int beta)
        {
            char? winner = board.CheckWinnerPublic();
            if (winner != null)
            {
                if (winner == this.Symbol) return 10 - depth;
                else return depth - 10; // tegenstander wint
            }

            if (board.IsFull()) return 0; // gelijkspel

            if (isMaximizing)
            {
                int maxEval = int.MinValue;
                foreach (var (r, c) in board.GetEmptyCells())
                {
                    board.PlaceSymbol(r, c, this.Symbol);
                    int eval = Minimax(board, depth + 1, false, alpha, beta);
                    board.RemoveSymbol(r, c);
                    maxEval = Math.Max(maxEval, eval);
                    alpha = Math.Max(alpha, eval);
                    if (beta <= alpha) break; // beta cut
                }
                return maxEval;
            }
            else
            {
                int minEval = int.MaxValue;
                char opponentSymbol = this.Symbol == 'X' ? 'O' : 'X';
                foreach (var (r, c) in board.GetEmptyCells())
                {
                    board.PlaceSymbol(r, c, opponentSymbol);
                    int eval = Minimax(board, depth + 1, true, alpha, beta);
                    board.RemoveSymbol(r, c);
                    minEval = Math.Min(minEval, eval);
                    beta = Math.Min(beta, eval);
                    if (beta <= alpha) break; // alpha cut
                }
                return minEval;
            }
        }
    }
}
