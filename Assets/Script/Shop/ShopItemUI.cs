using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemUI : MonoBehaviour
{
    public Image iconImage;
    public TMP_Text itemNameText;
    public TMP_Text priceText;
    public Button buyButton;

    private Items shopItem; // Mengganti ShopItemSO menjadi Items

    // Method untuk menampilkan informasi item pada shop
    public void SetShopItem(Items item) // Mengganti ShopItemSO menjadi Items
    {
        shopItem = item;

        if (iconImage != null)
        {
            iconImage.sprite = item.icon;
        }

        if (itemNameText != null)
        {
            itemNameText.text = item.itemName;
        }

        if (priceText != null)
        {
            priceText.text = "Price: " + item.price.ToString() + " coins"; // Pastikan ada properti price di dalam Items
        }

        if (buyButton != null)
        {
            buyButton.onClick.AddListener(BuyItem);
        }
    }

    // Method untuk menangani pembelian item
    private void BuyItem()
    {
        if (shopItem != null)
        {
            ShopManager.Instance.BuyItem(shopItem);
        }
    }
}