using System.Collections.Generic;

namespace Domain.Models
{
    public class PlayerScore
    {
        public string PlayerName { get; set; }
        public Dictionary<string, int> Scores { get; set; } = [];

        public PlayerScore(string playerName)
        {
            PlayerName = playerName;
            Scores = new Dictionary<string, int>
            {
                { "easy", 0 },
                { "unbeatable", 0 }
            };
        }

        public void AddWin(string aiLevel)
        {
            if (!Scores.ContainsKey(aiLevel))
                Scores[aiLevel] = 0;

            Scores[aiLevel]++;
        }

        public int GetScore(string aiLevel)
        {
            return Scores.ContainsKey(aiLevel) ? Scores[aiLevel] : 0;
        }
    }
}
