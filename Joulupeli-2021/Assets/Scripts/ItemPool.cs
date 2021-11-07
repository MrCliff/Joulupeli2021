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

        private readonly ICollection<CatchableItem> itemsInUse = new List<CatchableItem>();
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
            itemsInUse.Add(item);
            return item;
        }

        public CatchableItem GetItem(ItemProperties properties)
        {
            CatchableItem item = GetItem();
            item.Properties = properties;
            return item;
        }
    }
}
