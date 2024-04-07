using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    public List<Items> shopItems = new List<Items>(); // Mengganti List<ShopItemSO> menjadi List<Items>
    private int coins = 100;

    private void Awake()
    {
        Instance = this;
    }

   public void BuyItem(Items item)
{
    if (coins >= item.price)
    {
        InventoryManager.Instance.AddItem(item);
        coins -= item.price;
        UpdateCoinDisplay();
    }
    else
    {
        Debug.Log("Not enough coins to buy this item.");
    }
}
public void AddDailyCoins(int amount)
{
    coins += amount;
    UpdateCoinDisplay();
    Debug.Log($"Added {amount} coins. Total coins: {coins}");
}
    public int GetCoins()
    {
        return coins;
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
