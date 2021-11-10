﻿using System;
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
            Debug.Log(textDisplay);
        }

        private void Start()
        {
            GameMemory.Instance.PointCount += UpdatePointCount;
            UpdatePointCount(GameMemory.Instance.Points);
        }

        private void UpdatePointCount(int points)
        {
            textDisplay.text = string.Format("{0:N0}", points);
        }
    }
}