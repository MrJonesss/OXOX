using Domain.Interfaces.Data;
using Domain.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Data.Repository
{
    public class PlayerScoreRepository : IPlayerScoreRepository
    {
        private readonly string _filePath;

        public PlayerScoreRepository(string filePath)
        {
            _filePath = filePath;

            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");
        }

        public IEnumerable<PlayerScore> GetAll()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<PlayerScore>>(json) ?? new List<PlayerScore>();
        }

        public PlayerScore GetByName(string playerName)
        {
            return GetAll().FirstOrDefault(p => p.PlayerName == playerName);
        }

        public void Save(PlayerScore playerScore)
        {
            var scores = GetAll().ToList();
            var existing = scores.FirstOrDefault(p => p.PlayerName == playerScore.PlayerName);
            if (existing != null)
                scores.Remove(existing);

            scores.Add(playerScore);

            File.WriteAllText(_filePath, JsonSerializer.Serialize(scores, new JsonSerializerOptions { WriteIndented = true }));
        }

        public void SaveAll(IEnumerable<PlayerScore> scores)
        {
            File.WriteAllText(_filePath, JsonSerializer.Serialize(scores, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
