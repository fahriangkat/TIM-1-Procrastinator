using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftStationDoubleClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2) // Double click detected
        {
            MoveItemToInventory();
        }
    }

    private void MoveItemToInventory()
    {
        // Ambil referensi ke item ScriptableObject dari sprite icon pada Image
        Image image = GetComponent<Image>();
        Sprite iconSprite = image.sprite;
        Items itemData = GetItemDataFromIcon(iconSprite);

        // Pastikan item data tidak null
        if (itemData != null)
        {
            // Pindahkan item dari craft station ke inventory
            InventoryManager.Instance.AddItem(itemData);

            // Hapus item dari craft station
            CraftStation.Instance.RemoveItem(itemData);
        }
        else
        {
            Debug.LogWarning("Item data not found!");
        }
    }

    // Metode untuk mendapatkan data item dari sprite icon
    private Items GetItemDataFromIcon(Sprite icon)
    {
        // Lakukan pencarian data item berdasarkan sprite icon
        foreach (Items item in CraftStation.Instance.Items)
        {
            if (item.icon == icon)
            {
                return item;
            }
        }

        return null; // Return null jika tidak ditemukan
    }
}
