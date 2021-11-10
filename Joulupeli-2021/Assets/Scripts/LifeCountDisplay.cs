using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Displays the number of player lives.
    /// </summary>
    public class LifeCountDisplay : MonoBehaviour
    {
        [SerializeField]
        private LifeIndicator[] indicatorsFromFirstToLast;

        private void Start()
        {
            GameMemory.Instance.PlayerLifeCount += UpdateLifeIndicators;
            UpdateLifeIndicators(GameMemory.Instance.PlayerLives);
        }

        private void UpdateLifeIndicators(int lives)
        {
            for (int i = 0; i < indicatorsFromFirstToLast.Length; i++)
            {
                indicatorsFromFirstToLast[i].HasLife = lives > i;
            }
        }
    }
}
