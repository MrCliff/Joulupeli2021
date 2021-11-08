using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// An item that can be catched (by clicking on it).
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class CatchableItem : MonoBehaviour
    {
        [SerializeField]
        private ItemProperties properties;

        private SpriteRenderer spriteRenderer;

        /// <summary>
        /// Get or set current item properties that define the behavior of this item.
        /// </summary>
        public ItemProperties Properties
        {
            get { return properties; }
            set
            {
                properties = value;
                AssignProperties(properties);
            }
        }

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if (properties == null)
            {
                Debug.LogError("No item properties defined for a catchable item: " + name);
            }
        }

        private void AssignProperties(ItemProperties props)
        {
            spriteRenderer.sprite = props.Sprite;
        }
    }
}
