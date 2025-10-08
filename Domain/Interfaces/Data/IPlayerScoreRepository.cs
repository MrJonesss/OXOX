using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Data
{
    public interface IPlayerScoreRepository
    {
        IEnumerable<PlayerScore> GetAll();
        PlayerScore GetByName(string playerName);
        void Save(PlayerScore playerScore);
        void SaveAll(IEnumerable<PlayerScore> scores);
    }
}
