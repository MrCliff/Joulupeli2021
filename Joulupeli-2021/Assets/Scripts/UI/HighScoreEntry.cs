using Assets.Scripts.Data;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    /// <summary>
    /// Controls a single high score entry.
    /// </summary>
    public partial class HighScoreEntry : MonoBehaviour
    {

        [SerializeField]
        private TMP_Text orderNumberLabel;

        [SerializeField]
        private TMP_Text playerNameLabel;

        [SerializeField]
        private TMP_Text scoreLabel;

        public void SetOrderNumber(int orderNumber)
        {
            orderNumberLabel.text = orderNumber + ".";
        }

        /// <summary>
        /// Sets the high score shown on this entry.
        /// </summary>
        /// <param name="highScore">High score of a player.</param>
        public void SetHighScore(HighScore highScore)
        {
            if (highScore != null)
            {
                playerNameLabel.text = highScore.PlayerName;
                scoreLabel.text = string.Format("{0:N0}", highScore.Score);
            }
            else
            {
                playerNameLabel.text = "-";
                scoreLabel.text = "0";
            }
        }
    }
}