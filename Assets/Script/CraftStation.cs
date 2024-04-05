using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftStation : MonoBehaviour
{
    [SerializeField]
    public static CraftStation Instance;

    public List<Items> Items = new List<Items>(); // List to store items in the craft station
    public Transform CraftContent; // Parent transform for UI items in the craft station
    public GameObject CraftItemPrefab; // Prefab for UI item in the craft station
    public Toggle EnableRemove; // Toggle to enable/disable item removal

    private int maxCapacity = 3; // Maximum capacity of the craft station

    private void Awake()
    {
        Instance = this;
        ListItem();
        // Add initial items to the craft station
    }

    // Method to check if a recipe can be crafted
    public bool CanCraft(CraftRecipe recipe)
    {
        if (recipe == null)
        {
            Debug.LogError("Recipe is null.");
            return false;
        }

        foreach (var ingredient in recipe.requiredIngredients)
        {
            if (!HasIngredient(ingredient))
            {
                Debug.Log($"Missing ingredient: {ingredient.itemName}");
                return false;
            }
        }

        return true;
    }

    // Method to craft an item based on a recipe
    public void CraftItem(CraftRecipe recipe)
    {
        if (CanCraft(recipe))
        {
            // Remove all ingredients used from the craft station
            foreach (Items ingredient in recipe.requiredIngredients)
            {
                RemoveItem(ingredient);
            }
            // Add the resulting item to the craft station
            AddItem(recipe.resultingItem);
        }
        else
        {
            Debug.Log("Cannot craft " + recipe.recipeName + ". Missing ingredients.");
        }
    }

    // Method to add an item to the craft station
   public void AddItem(Items item)
{
    if (Items.Count < maxCapacity)
    {
        Items.Add(item);
        Debug.Log("Item added to craft station: " + item.itemName);
        // Update the UI to reflect the addition of the item
        ListItem(); // Panggil metode ListItem di sini
    }
    else
    {
        Debug.Log("Craft station is full. Cannot add more items.");
    }
}

    // Method to remove an item from the craft station
   public void RemoveItem(Items item)
{
    Items.Remove(item);
    // Update the UI to reflect the removal of the item
    ListItem(); // Panggil metode ListItem di sini
}
    // Method to check if the craft station has a specific ingredient
    public bool HasIngredient(Items ingredient)
    {
        return Items.Contains(ingredient);
    }

    // Method to update the UI with the items in the craft station
    public void ListItem()
    {
        // Clear existing UI elements
        foreach (Transform item in CraftContent)
        {
            Destroy(item.gameObject);
        }

        // Instantiate UI elements for each item in the craft station
        int itemCountToShow = Mathf.Min(Items.Count, maxCapacity); // Ensure only maxCapacity items are displayed
        for (int i = 0; i < itemCountToShow; i++)
        {
            Items item = Items[i];
            GameObject itemObj = Instantiate(CraftItemPrefab, CraftContent);
            if (itemObj == null)
            {
                Debug.LogWarning("Failed to instantiate CraftItem prefab!");
                return;
            }

            // Get references to UI components and update them
            var itemName = itemObj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = itemObj.transform.GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }

    // Method to mark that an item has been moved to the craft station
    public void MarkItemMovedToCraftStation(Items item)
    {
        // Add implementation as needed
    }

    // Other necessary methods can be added here
}
