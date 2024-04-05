using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    public GameObject content; // Container untuk item-item pada shop
    public GameObject shopItemPrefab; // Prefab untuk item pada shop

    private void Start()
    {
        PopulateShop();
    }

    // Method untuk menampilkan item-item pada shop
    private void PopulateShop()
    {
        if (content == null || shopItemPrefab == null)
        {
            Debug.LogError("Content or shop item prefab is not assigned.");
            return;
        }

        // Dapatkan referensi ke ShopManager
        ShopManager shopManager = ShopManager.Instance;
        if (shopManager == null)
        {
            Debug.LogError("ShopManager instance is null.");
            return;
        }

        // Dapatkan daftar item pada shop dari ShopManager
        var shopItems = shopManager.shopItems;

        // Loop melalui setiap item pada shopItems
        foreach (var shopItem in shopItems)
        {
            // Instantiate prefab untuk item pada shop
            GameObject itemObj = Instantiate(shopItemPrefab, content.transform);
            if (itemObj == null)
            {
                Debug.LogError("Failed to instantiate shop item prefab.");
                continue;
            }

            // Dapatkan komponen ShopItemUI dari prefab yang di-instantiate
            ShopItemUI shopItemUI = itemObj.GetComponent<ShopItemUI>();
            if (shopItemUI == null)
            {
                Debug.LogError("Shop item prefab does not contain ShopItemUI component.");
                continue;
            }

            // Setel informasi item pada ShopItemUI
            shopItemUI.SetShopItem(shopItem);
        }
    }
}
