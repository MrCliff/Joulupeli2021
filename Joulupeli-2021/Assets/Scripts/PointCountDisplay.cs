using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Displays the current points.
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class PointCountDisplay : MonoBehaviour
    {
        private TMP_Text textDisplay;

        private void Awake()
        {
            textDisplay = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            GameMemory.Instance.OnPointCountChange += UpdatePointCount;
            UpdatePointCount(GameMemory.Instance.Points);
        }

        private void OnDestroy()
        {
            GameMemory.Instance.OnPointCountChange -= UpdatePointCount;
        }

        private void UpdatePointCount(int points)
        {
            textDisplay.text = string.Format("{0:N0}", points);
        }
    }
}
