using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class BackpackHandler : MonoBehaviour
    {
        public UnityEvent<ItemData> OnItemAdded;
        public UnityEvent<ItemData> OnItemRemoved;
        private Dictionary<ItemData, int> items;

        private void Awake()
        {
            items = new Dictionary<ItemData, int>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Draggable"))
            {
                Add(other.gameObject);
                Destroy(other.gameObject);
                Debug.Log($"Item {other.name} placed in the backpack.");
            }
        }

        private void Add(GameObject itemObj)
        {
            var itemData = itemObj.GetComponent<PickupItem>()?._itemData;
            if (itemData == null)
            {
                Debug.LogError("ItemData is missing!");
                return;
            }

            if (!items.ContainsKey(itemData))
            {
                items.Add(itemData, 1);
            }
            else
            {
                items[itemData]++;
            }
            OnItemAdded?.Invoke(itemData);
        }

        public void Remove(ItemData itemData)
        {
            if (!items.ContainsKey(itemData))
            {
                Debug.LogError("Item not found in backpack.");
                return;
            }

            if (items[itemData] == 1)
            {
                items.Remove(itemData);
            }
            else
            {
                items[itemData]--;
            }

            OnItemRemoved?.Invoke(itemData);
        }

        public Dictionary<ItemData, int> GetContent() => new Dictionary<ItemData, int>(items);
    }

    
}