using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Stores values between scene loads.
    /// </summary>
    public class GameMemory : MonoBehaviour
    {
        [SerializeField]
        private int points = 0;

        [SerializeField]
        private int initialPlayerLives = 3;

        private int playerLives;

        /// <summary>
        /// Returns the currently active GameMemory.
        /// </summary>
        public static GameMemory Instance { get; private set; }

        /// <summary>
        /// Current point count of the player.
        /// </summary>
        public int Points
        {
            get { return points; }
            private set
            {
                points = value;
                OnPointCountChange?.Invoke(points);
            }
        }

        /// <summary>
        /// Event for listening to the change of point count.
        /// </summary>
        public event Action<int> OnPointCountChange;

        /// <summary>
        /// Count of lives the player currently has.
        /// </summary>
        public int PlayerLives
        {
            get { return playerLives; }
            private set
            {
                playerLives = value;
                OnPlayerLifeCountChange?.Invoke(playerLives);

                if (playerLives <= 0)
                {
                    GameController.FindInstance().EndGameAsFailure();
                }
            }
        }

        /// <summary>
        /// Event for listening to the change of player life count.
        /// </summary>
        public event Action<int> OnPlayerLifeCountChange;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            ResetLives();
        }

        /// <summary>
        /// Adds the given points to the total points count.
        /// </summary>
        /// <param name="points">Points to add.</param>
        public void AddPoints(int points)
        {
            Points += points;
        }

        /// <summary>
        /// Resets the point count to 0.
        /// </summary>
        public void ResetPoints()
        {
            Points = 0;
        }

        /// <summary>
        /// Takes one life from the active player lives.
        /// </summary>
        public void TakeALife()
        {
            PlayerLives--;
        }

        /// <summary>
        /// Resets the life count to the initial value.
        /// </summary>
        public void ResetLives()
        {
            PlayerLives = initialPlayerLives;
        }
    }
}
