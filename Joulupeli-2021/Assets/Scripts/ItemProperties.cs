using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// An object that holds properties for a <see cref="CatchableItem"/>.
    /// </summary>
    [CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemProperties")]
    public class ItemProperties : ScriptableObject
    {
        [SerializeField]
        private Sprite sprite;

        [SerializeField]
        private int points = 1;

        [SerializeField]
        private bool isGood = true;

        public Sprite Sprite { get { return sprite; } }
        public int Points { get { return points; } }
        public bool IsGood { get { return isGood; } }
    }
}
