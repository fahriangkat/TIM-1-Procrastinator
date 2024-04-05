using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    public static InventoryManager Instance;

    public List<Items> Items = new List<Items>(); // List to store items
    public Transform InventoryContent; // Container for UI items in the inventory
    public GameObject InventoryItem; // Prefab for UI item in the inventory
    public Toggle EnableRemove;

    private void Awake()
    {
        Instance = this;

        // Add initial items to the inventory
    }

    public void AddItem(Items item)
{
    Items.Add(item);
    Debug.Log("Item added to inventory: " + item.itemName);
    // After adding an item, update the inventory UI
    ListItem(); // Panggil metode ListItem di sini
}

   public void Remove(Items item)
{
    Items.Remove(item);
    // After removing an item, update the inventory UI
    ListItem(); // Panggil metode ListItem di sini
}
    public void ListItem()
    {
        // Check if InventoryContent is null
        if (InventoryContent == null)
        {
            Debug.LogWarning("InventoryContent is not assigned!");
            return;
        }

        // Clear existing UI elements
        foreach (Transform item in InventoryContent)
        {
            Destroy(item.gameObject);
        }

        // Instantiate UI elements for each item in the inventory
        foreach (var item in Items)
        {
            // Null-check InventoryItem prefab
            if (InventoryItem == null)
            {
                Debug.LogWarning("InventoryItem prefab is not assigned!");
                return;
            }

            // Instantiate UI element with a valid parent (InventoryContent)
            GameObject itemObj = Instantiate(InventoryItem, InventoryContent);
            if (itemObj == null)
            {
                Debug.LogWarning("Failed to instantiate InventoryItem prefab!");
                return;
            }

            // Get references to UI components and update them
            var itemName = itemObj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = itemObj.transform.GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }

        // Update the visibility of remove buttons
        EnableItemsRemove();
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove == null)
        {
            Debug.LogWarning("EnableRemove is not assigned!");
            return;
        }

        foreach (Transform item in InventoryContent)
        {
            GameObject removeButton = item.Find("RemoveButton")?.gameObject;
            if (removeButton != null)
            {
                removeButton.SetActive(EnableRemove.isOn);
            }
        }
    }
}
