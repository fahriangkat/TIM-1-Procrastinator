using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    // List of items in the shop
    public List<Items> shopItems = new List<Items>();

    private int coins = 100; // Default starting coins

    private string coinsSaveKey = "PlayerCoins"; // Key for saving coins

    private void Awake()
    {
        Instance = this;
        LoadCoins(); // Load player coins when the game starts
    }

    private void LoadCoins()
    {
        if (PlayerPrefs.HasKey(coinsSaveKey))
        {
            coins = PlayerPrefs.GetInt(coinsSaveKey);
        }
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt(coinsSaveKey, coins);
        PlayerPrefs.Save();
    }

    public void BuyItem(Items item)
    {
        if (Instance != null)
        {
            if (Instance.coins >= item.price)
            {
                InventoryManager.Instance.AddItem(item);
                Instance.coins -= item.price;
                SaveCoins(); // Save player coins after buying an item
                UpdateCoinDisplay();
            }
            else
            {
                Debug.Log("Not enough coins to buy this item.");
            }
        }
        else
        {
            Debug.LogError("ShopManager Instance is not assigned.");
        }
    }

    public void BuyRecipeItem(RecipeItem item)
    {
        if (Instance != null)
        {
            if (Instance.coins >= item.price)
            {
                InventoryManager.Instance.AddItem(item);
                Instance.coins -= item.price;

                // After adding the item to inventory, unlock the corresponding recipe
                CraftRecipe correspondingRecipe = item.correspondingRecipe;
                if (correspondingRecipe != null)
                {
                    correspondingRecipe.isUnlocked = true;
                    Debug.Log($"Craft recipe unlocked: {correspondingRecipe.recipeName}");
                }
                else
                {
                    Debug.Log("Matching recipe not found for purchased item.");
                }

                SaveCoins(); // Save player coins after buying a recipe item
                UpdateCoinDisplay();
            }
            else
            {
                Debug.Log("Not enough coins to buy this item.");
            }
        }
        else
        {
            Debug.LogError("ShopManager Instance is not assigned.");
        }
    }

    public void AddCoins(int amount)
    {
        if (Instance != null)
        {
            Instance.coins += amount;
            SaveCoins(); // Save player coins after adding coins
            UpdateCoinDisplay(); // Update the coin display after adding coins
        }
        else
        {
            Debug.LogError("ShopManager Instance is not assigned.");
        }
    }

    public void SellItem(Items item)
    {
        coins += item.sellPrice; // Add sell price to coins when item is sold
        InventoryManager.Instance.Remove(item);
        SaveCoins(); // Save player coins after selling an item
        UpdateCoinDisplay();
    }

    public int GetCoins()
    {
        return coins;
    }

    public void AddDailyCoins(int amount)
    {
        if (Instance != null)
        {
            Instance.coins += amount;
            SaveCoins(); // Save player coins after adding daily coins
            UpdateCoinDisplay();
            Debug.Log($"Added {amount} coins. Total coins: {Instance.coins}");
        }
        else
        {
            Debug.LogError("ShopManager Instance is not assigned.");
        }
    }

    private void UpdateCoinDisplay()
    {
        CoinDisplay coinDisplay = FindObjectOfType<CoinDisplay>();
        if (coinDisplay != null)
        {
            coinDisplay.UpdateCoinDisplay();
        }
    }
}
