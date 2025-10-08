using Domain.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class AiPlayer : Player, IAiPlayer
    {
        public AiPlayer(string name, char symbol)
            : base(name, symbol, isAi: true) { }

        public virtual (int row, int col) GetMove(Board board)
        {
            // Voor nu dummy: altijd (0,0)
            return (0, 0);
        }
    }
}
