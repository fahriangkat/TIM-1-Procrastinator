using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    public static InventoryManager Instance;

    public List<CraftRecipe> Recipes = new List<CraftRecipe>(); // List to store recipes
    
    public List<Items> Items = new List<Items>(); // List to store items
    public Transform InventoryContent; // Container for UI items in the inventory
    public GameObject InventoryItem; // Prefab for UI item in the inventory
    public Toggle EnableRemove;
    private ShopManager shopManager;
    public Button sellButton;
    
    public CoinDisplay coinDisplay;

    private string inventorySaveKey = "Inventory"; // Key for saving inventory data
    
    private void Awake()
    {
        Instance = this;
        shopManager = ShopManager.Instance;
        // Add initial items to the inventory
        LoadInventory(); // Load inventory data when the game starts
    }

    // Method to save inventory data
    public void SaveInventory()
    {
        // Convert Items list to JSON string
        string inventoryJson = JsonUtility.ToJson(Items);
        // Save JSON string to PlayerPrefs
        PlayerPrefs.SetString(inventorySaveKey, inventoryJson);
        PlayerPrefs.Save();
    }

    // Method to load inventory data
    public void LoadInventory()
    {
        if (PlayerPrefs.HasKey(inventorySaveKey))
        {
            // Get JSON string from PlayerPrefs
            string inventoryJson = PlayerPrefs.GetString(inventorySaveKey);
            // Convert JSON string back to Items list
            Items = JsonUtility.FromJson<List<Items>>(inventoryJson);
            // Update inventory UI after loading inventory data
            ListItem();
        }
    }

    public void AddRecipe(CraftRecipe recipe)
    {
        Recipes.Add(recipe);
        Debug.Log("Recipe added to inventory: " + recipe.recipeName);
        // After adding a recipe, update the inventory UI
        ListItem();
        // Save inventory data after adding recipe
        SaveInventory();
    }

    public void AddItem(Items item)
    {
        Items.Add(item);
        Debug.Log("Item added to inventory: " + item.itemName);
        // After adding an item, update the inventory UI
        ListItem();
        // Save inventory data after adding item
        SaveInventory();
    }

    public void Remove(Items item)
    {
        Items.Remove(item);
        // After removing an item, update the inventory UI
        ListItem();
        // Save inventory data after removing item
        SaveInventory();
    }

    public void ListItem()
    {
        if (InventoryContent == null)
        {
            Debug.LogWarning("InventoryContent is not assigned!");
            return;
        }

        foreach (Transform item in InventoryContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in Items)
        {
            GameObject itemObj = Instantiate(InventoryItem, InventoryContent);
            if (itemObj == null)
            {
                Debug.LogWarning("Failed to instantiate InventoryItem prefab!");
                continue;
            }

            var itemNameText = itemObj.transform.Find("ItemName").GetComponent<TMPro.TextMeshProUGUI>();
            var itemIcon = itemObj.transform.GetComponent<Image>();

            itemNameText.text = item.itemName;
            itemIcon.sprite = item.icon;
            itemObj.GetComponent<Button>().onClick.AddListener(() => SelectItem(item));

            if (item is RecipeItem)
            {
                // If the item is a recipe, handle it differently
                RecipeItem recipeItem = item as RecipeItem;
                // Set isUnlocked to true for the corresponding recipe
                if (recipeItem.correspondingRecipe != null)
                {
                    recipeItem.correspondingRecipe.isUnlocked = true;
                    Debug.Log($"Craft recipe unlocked: {recipeItem.correspondingRecipe.recipeName}");
                }
                // Example: Add click listener to show recipe info
                itemObj.GetComponent<Button>().onClick.AddListener(() => ShowRecipeInfo(recipeItem.correspondingRecipe));
            }
        }

        EnableItemsRemove();
    }
    
    public void SellSelectedItem()
    {
        // Cek apakah ada item yang dipilih
        Items selectedItem = GetSelectedItem();
        if (selectedItem != null)
        {
            // Jual item yang dipilih dan hapus dari inventori
            coinDisplay.AddCoins(selectedItem.sellPrice);
            Remove(selectedItem);
        }
        else
        {
            Debug.Log("No item selected to sell.");
        }
    }


    private void SelectItem(Items item)
    {
        // Clear previous selection
        foreach (var i in Items)
        {
            i.isSelected = false;
        }

        // Set the selected item
        item.isSelected = true;

        // Update UI
        ListItem();
    }

    public Items GetSelectedItem()
    {
        foreach (var item in Items)
        {
            if (item.isSelected)
            {
                return item;
            }
        }
        return null;
    }

    public void ShowRecipeInfo(CraftRecipe recipe)
    {
        // Implement method to show recipe information panel
    }

    public void RemoveItem(Items item)
    {
        Items.Remove(item);
        Debug.Log($"Item removed from inventory: {item.itemName}");

        // If the removed item is a RecipeItem, set isUnlocked to false for its corresponding recipe
        if (item is RecipeItem)
        {
            RecipeItem recipeItem = item as RecipeItem;
            if (recipeItem.correspondingRecipe != null)
            {
                recipeItem.correspondingRecipe.isUnlocked = false;
                Debug.Log($"Craft recipe locked: {recipeItem.correspondingRecipe.recipeName}");
            }
        }

        // Update the inventory UI after removing an item
        ListItem();
    }

    // Add this method to handle remove button click event
    public void RemoveItemButtonClicked(Items item)
    {
        RemoveItem(item);
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
