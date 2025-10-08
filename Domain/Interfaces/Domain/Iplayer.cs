using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Domain
{
    public interface Iplayer
    {
        string Name { get; }
        char Symbol { get; }
        bool IsAi { get; set; }
        int Score { get; set; }
        void AddWin();
        string ToString();
    }
}
