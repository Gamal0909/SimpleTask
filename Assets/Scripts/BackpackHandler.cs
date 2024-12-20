using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public class BackpackHandler : MonoBehaviour
    {
        public UnityEngine.Events.UnityEvent<ItemData, string> onItemEvent;
        private Dictionary<string, int> items;
        private void Awake()
        {
            items = new Dictionary<string, int>();
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
            var itemData = itemObj.GetComponent<PickupItem>()._itemData;
            if (!items.ContainsKey(itemData.itemName) &&itemData != null)
            {
                items.Add(itemData.itemName,1);
            }
            else
            {
                items[itemData.itemName]++;
            }
            onItemEvent?.Invoke(itemData,"Added");
            GetContent();
        }
        private void Remove(GameObject itemObj)
        {
            var itemData = itemObj.GetComponent<PickupItem>()._itemData;
            if (!items.ContainsKey(itemData.itemName) &&itemData != null)
            {
                Debug.LogError("Error on add");
            }
            else if (items[itemData.itemName]==1)
            {
                items.Remove(itemData.itemName);
            }
            else
            {
                items[itemData.itemName]++;
            }
            onItemEvent?.Invoke(itemData,"Remove");
        }

        void GetContent()
        {
            foreach (var item in items)
                Debug.Log($"{item.Key}:{item.Value}" );
        }
    }
}