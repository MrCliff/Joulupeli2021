using System;

namespace Assets.Scripts.Data
{
    /// <summary>
    /// Contains data for a high score.
    /// </summary>
    [Serializable]
    public class HighScore
    {

        private readonly string playerName;
        private readonly int score;

        /// <summary>
        /// Name of the player that got the score.
        /// </summary>
        public string PlayerName { get { return playerName; } }
        /// <summary>
        /// Score of the player.
        /// </summary>
        public int Score { get { return score; } }

        public HighScore(string playerName, int score)
        {
            this.playerName = playerName;
            this.score = score;
        }
    }
}