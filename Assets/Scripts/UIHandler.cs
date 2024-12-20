using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject bagButton;
    [SerializeField] private GameObject itemspage;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject items;
    [SerializeField] private BackpackHandler backpack;
    [SerializeField] private Transform spawnPoint; 
    [SerializeField] private int buttonSpacing = 30; 
    [SerializeField] private ServerCommunication serverCommunication;
    void Awake()
    {
        bagButton.SetActive(true);
        itemspage.SetActive(false);
        backpack.OnItemAdded.AddListener(OnItemAdded);
        backpack.OnItemRemoved.AddListener(OnItemRemoved);
    }

    private void OnItemRemoved(ItemData itemData)
    {
        serverCommunication.SendItemToServer(itemData, "remove");
        Debug.Log($"{itemData.itemName} removed from backpack.");
        PopulateInventory();
    }

    private void OnItemAdded(ItemData itemData)
    {
        serverCommunication.SendItemToServer(itemData, "add");
        Debug.Log($"{itemData.itemName} added to backpack.");
        PopulateInventory();
    }

    public void BagButton()
    {
        bagButton.SetActive(false);
        itemspage.SetActive(true);
        PopulateInventory();
        Time.timeScale = 0f; 
    }

    public void Back()
    {
        bagButton.SetActive(true);
        itemspage.SetActive(false);
        Time.timeScale = 1f; 
    }
    
    public void PopulateInventory()
    {
        Dictionary<ItemData, int> itemsData = backpack.GetContent();
        foreach (Transform child in items.transform)
        {
            Destroy(child.gameObject);
        }
        int currentHeight = 0;
        foreach (var item in itemsData)
        {
            GameObject newButton = Instantiate(buttonPrefab, items.transform);
            RectTransform buttonTransform = newButton.GetComponent<RectTransform>();
            buttonTransform.anchoredPosition = new Vector2(0, -currentHeight);
            currentHeight += buttonSpacing;
            newButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = $"{item.Key.itemName} : {item.Value}";
            newButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
            {
                backpack.Remove(item.Key);
                Instantiate(item.Key.prefab, spawnPoint.position, Quaternion.identity);
                PopulateInventory(); // Refresh inventory after removing the item
            });
        }
    }
}
