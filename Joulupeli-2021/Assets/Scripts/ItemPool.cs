using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Pool for items. This makes it possible to reuse same item objects without destroying them.
    /// </summary>
    [Serializable]
    public class ItemPool
    {
        [SerializeField]
        GameObject catchableItemPrefab;

        private readonly ICollection<CatchableItem> itemsInUse = new HashSet<CatchableItem>();
        private readonly Queue<CatchableItem> itemsIdle = new Queue<CatchableItem>();

        /// <summary>
        /// Returns an item from the pool. If none available, creates new item.
        /// </summary>
        /// <returns>An item.</returns>
        public CatchableItem GetItem()
        {
            CatchableItem item;
            if (itemsIdle.Count > 0)
            {
                item = itemsIdle.Dequeue();
            }
            else
            {
                GameObject go = UnityEngine.Object.Instantiate(catchableItemPrefab);
                item = go.GetComponent<CatchableItem>();
            }
            item.ItemPool = this;
            item.gameObject.SetActive(true);
            itemsInUse.Add(item);
            return item;
        }

        public CatchableItem GetItem(ItemProperties properties)
        {
            CatchableItem item = GetItem();
            item.Properties = properties;
            return item;
        }

        /// <summary>
        /// "Destroys" the given item. Actually removes it from active items and adds it to the pool.
        /// </summary>
        /// <param name="item"></param>
        public void Destroy(CatchableItem item)
        {
            item.gameObject.SetActive(false);
            itemsInUse.Remove(item);
            itemsIdle.Enqueue(item);
        }
    }
}
