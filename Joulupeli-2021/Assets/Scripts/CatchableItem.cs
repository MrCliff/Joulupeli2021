using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    /// <summary>
    /// An item that can be catched (by clicking on it).
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class CatchableItem : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField]
        private ItemProperties properties;

        private SpriteRenderer spriteRenderer;
        private BoxCollider2D triggerCollider;
        private new Rigidbody2D rigidbody;

        public ItemPool ItemPool { private get; set; }

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
            triggerCollider = GetComponent<BoxCollider2D>();
            rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            if (properties == null)
            {
                Debug.LogError("No item properties defined for a catchable item: " + name);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            GameMemory.Instance.AddPoints(properties.Points);
            Debug.Log($"NAPS! Current points: {GameMemory.Instance.Points}");
            ItemPool.Destroy(this);
        }

        public void AssignRandomPropertiesFrom(CommonProperties commonItemProperties)
        {
            rigidbody.drag = commonItemProperties.RandomLinearDrag();
            rigidbody.angularDrag = commonItemProperties.RandomAngularDrag();
            rigidbody.gravityScale = commonItemProperties.RandomGravityScale();
            rigidbody.angularVelocity = commonItemProperties.RandomAngularVelocity();
        }

        private void AssignProperties(ItemProperties props)
        {
            spriteRenderer.sprite = props.Sprite;
            if (triggerCollider)
            {
                triggerCollider.size = spriteRenderer.sprite.bounds.size;
            }
        }

        /// <summary>
        /// Struct for holding item properties that are common for all CatchableItems.
        /// </summary>
        [Serializable]
        public struct CommonProperties
        {
            [Min(0)]
            public float minLinearDrag;
            [Min(0)]
            public float maxLinearDrag;
            [Min(0)]
            public float minAngularDrag;
            [Min(0)]
            public float maxAngularDrag;
            public float minGravityScale;
            public float maxGravityScale;
            public float minAngularVelocity;
            public float maxAngularVelocity;

            public float RandomLinearDrag()
            {
                return UnityEngine.Random.Range(minLinearDrag, maxLinearDrag);
            }

            public float RandomAngularDrag()
            {
                return UnityEngine.Random.Range(minAngularDrag, maxAngularDrag);
            }

            public float RandomGravityScale()
            {
                return UnityEngine.Random.Range(minGravityScale, maxGravityScale);
            }

            public float RandomAngularVelocity()
            {
                return UnityEngine.Random.Range(minAngularVelocity, maxAngularVelocity);
            }
        }
    }
}
