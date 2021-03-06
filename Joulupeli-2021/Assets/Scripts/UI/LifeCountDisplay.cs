using Assets.Scripts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.UI
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
            GameMemory.Instance.OnPlayerLifeCountChange += UpdateLifeIndicators;
            UpdateLifeIndicators(GameMemory.Instance.PlayerLives);
        }

        private void OnDestroy()
        {
            GameMemory.Instance.OnPlayerLifeCountChange -= UpdateLifeIndicators;
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
