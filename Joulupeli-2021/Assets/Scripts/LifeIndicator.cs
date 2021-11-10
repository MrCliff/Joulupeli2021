using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /// <summary>
    /// Represents a boolean indicator for one player life.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class LifeIndicator : MonoBehaviour
    {
        [SerializeField]
        private bool hasLife = true;

        [SerializeField]
        private float noLifeAlpha = 0.3f;

        private Image indicatorImage;

        /// <summary>
        /// If true, this is one active life of the player.
        /// </summary>
        public bool HasLife
        {
            get { return hasLife; }
            set
            {
                hasLife = value;
                RefreshIndicator();
            }
        }

        private void Awake()
        {
            UpdateReferences();
        }

        private void Start()
        {
            RefreshIndicator();
        }

        private void UpdateReferences()
        {
            indicatorImage = GetComponent<Image>();
        }

        private void RefreshIndicator()
        {
            Color color = indicatorImage.color;
            color.a = HasLife ? 1.0f : noLifeAlpha;
            indicatorImage.color = color;
        }

        private void OnDrawGizmosSelected()
        {
            UpdateReferences();
            RefreshIndicator();
        }
    }
}
