using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Image selectionIndicator; // Referensi ke indikator pemilihan pada item
    public Items item; // Ubah menjadi public agar dapat diakses dari luar kelas
 // Item yang terkait dengan InventoryItem

    public TMPro.TextMeshProUGUI sellPriceText; // Tambahkan teks harga jual

    // Metode untuk menetapkan item dan harga jualnya
    public void SetItem(Items newItem)
    {
        item = newItem; // Tetapkan item yang baru
        sellPriceText.text = "Sell: " + item.sellPrice; // Tampilkan harga jual
        // Lainnya ...
    }

    // Metode untuk membalik status pemilihan item
    public void ToggleSelection()
    {
        // Dapatkan referensi ke InventoryManager
        InventoryManager inventoryManager = InventoryManager.Instance;

        if (inventoryManager != null)
        {
            // Dapatkan item yang dipilih dari InventoryManager
            Items selectedItem = inventoryManager.GetSelectedItem();

            // Jika item yang dipilih sama dengan item yang terkait dengan InventoryItem, maka toggle pemilihan item
            if (selectedItem != null && selectedItem == item)
            {
                item.isSelected = !item.isSelected;

                // Perbarui tampilan item sesuai dengan status pemilihan
                if (item.isSelected)
                {
                    if (selectionIndicator != null)
                    {
                        selectionIndicator.gameObject.SetActive(true);
                    }
                }
                else
                {
                    if (selectionIndicator != null)
                    {
                        selectionIndicator.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    private void Start()
    {
        // Tambahkan listener untuk mendengarkan input klik pada objek InventoryItem
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ToggleSelection);
        }
    }
}
