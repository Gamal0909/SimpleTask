using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class Backpack : MonoBehaviour
    {
        public UnityEngine.Events.UnityEvent<ItemData, string> onItemEvent;
        public List<ItemData> items = new List<ItemData>();
        public Dictionary<ItemData, int> backpackItems = new Dictionary<ItemData, int>();
        public void AddItem(ItemData item)
        {
            
            items.Add(item);
            onItemEvent?.Invoke(item,"Added");
            
        }

        public void RemoveItem(ItemData item)
        {
            bool removed = items.Remove(item);
            if (removed)
                Debug.Log($"{item.itemName} is Removed");
            onItemEvent?.Invoke(item, "Removed");
        }

        public List<ItemData> GetContent()
        {
            return new List<ItemData>();
        }
    }
}