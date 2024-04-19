using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    // List of items in the shop
    public List<Items> shopItems = new List<Items>();

    public int coins { get; private set; } = 100;
    
    private void Awake()
    {
        Instance = this;
    }

    public void BuyItem(Items item)
    {
        if (Instance != null)
        {
            if (Instance.coins >= item.price)
            {
                InventoryManager.Instance.AddItem(item);
                Instance.coins -= item.price;
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
        UpdateCoinDisplay();
    }

    public int GetCoins()
    {
        if (Instance != null)
        {
            return Instance.coins;
        }
        else
        {
            Debug.LogError("ShopManager Instance is not assigned.");
            return 0;
        }
    }

    public void AddDailyCoins(int amount)
    {
        if (Instance != null)
        {
            Instance.coins += amount;
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