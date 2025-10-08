using Domain.Exceptions;
using Domain.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Player : Iplayer
    {
        public string Name { get; }
        public char Symbol { get; }
        public bool IsAi { get; set; }
        public int Score { get; set; }

        public Player(string name, char symbol, bool isAi = false)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new InvalidPlayerNameException(name);
            }

            if (symbol != 'X' && symbol != 'O')
            {
                throw new InvalidPlayerSymbolException(symbol);
            }

            Name = name;
            Symbol = symbol;
            IsAi = isAi;
            Score = 0;

        }

        public void AddWin()
        {
            Score++;
        }

        public override string ToString()
        {
            string type = IsAi ? "AI" : "Human";
            return $"{Name} {Symbol} - ({type}), Score: {Score}";
        }
    }
}
