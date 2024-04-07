using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryDoubleClick : MonoBehaviour, IPointerClickHandler
{
    private bool isClicked; // Menyimpan status klik
    private float lastClickTime; // Waktu terakhir klik terdeteksi

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isClicked)
        {
            // Jika belum ada klik sebelumnya, set status menjadi true dan simpan waktu klik terakhir
            isClicked = true;
            lastClickTime = Time.time;
        }
        else
        {
            // Jika ada klik sebelumnya, hitung selisih waktu antara klik terakhir dan klik saat ini
            float timeSinceLastClick = Time.time - lastClickTime;

            // Jika selisih waktu dalam rentang waktu yang ditentukan (misalnya 0.5 detik), maka double tap terdeteksi
            if (timeSinceLastClick <= 0.5f)
            {
                // Reset status dan waktu klik
                isClicked = false;
                lastClickTime = 0f;

                // Handle double tap
                HandleDoubleTap();
                return;
            }
            else
            {
                // Jika selisih waktu lebih besar dari rentang waktu yang ditentukan, tandai sebagai single tap dan simpan waktu klik terakhir
                isClicked = true;
                lastClickTime = Time.time;
            }
        }

        // Setelah menunggu rentang waktu yang ditentukan, reset status dan waktu klik
        Invoke("ResetClick", 0.5f);
    }

    private void ResetClick()
    {
        isClicked = false;
        lastClickTime = 0f;
    }

    private void HandleDoubleTap()
    {
        if (IsInInventoryMode())
        {
            MoveItemToCraftStation();
        }
        else if (IsInCraftStationMode())
        {
            MoveItemToInventory();
        }
    }

    private bool IsInInventoryMode()
    {
        return transform.parent == InventoryManager.Instance.InventoryContent;
    }

    private bool IsInCraftStationMode()
    {
        return transform.parent == CraftStation.Instance.CraftContent;
    }

    private void MoveItemToCraftStation()
    {
        // Ambil referensi ke item ScriptableObject dari sprite icon pada Image
        Image image = GetComponent<Image>();
        Sprite iconSprite = image.sprite;
        Items itemData = GetItemDataFromIcon(iconSprite);

        // Pastikan item data tidak null
        if (itemData != null)
        {
            // Pindahkan item dari inventory ke craft station
            CraftStation.Instance.AddItem(itemData);

            // Hapus item dari inventory
            InventoryManager.Instance.Remove(itemData);
        }
        else
        {
            Debug.LogWarning("Item data not found!");
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
        foreach (Items item in InventoryManager.Instance.Items)
        {
            if (item.icon == icon)
            {
                return item;
            }
        }

        // Lakukan pencarian data item berdasarkan sprite icon pada craft station
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
