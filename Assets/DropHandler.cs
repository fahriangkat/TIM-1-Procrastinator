using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler : MonoBehaviour, IDropHandler
{
    // Brew Station sebagai target drop
    public BrewStation brewStation;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItemDragAndDrop InventoryItemDragAndDrop = eventData.pointerDrag.GetComponent<InventoryItemDragAndDrop>();
        if (InventoryItemDragAndDrop != null)
        {
            // Memastikan bahwa objek yang di-drop adalah item yang dapat di-craft
            Items item = eventData.pointerDrag.GetComponent<Items>(); // Menggunakan Items sebagai tipe data untuk item
            if (item != null)
            {
                // Memindahkan objek ke Brew Station
                InventoryItemDragAndDrop.transform.SetParent(brewStation.transform);

                // Lakukan proses crafting jika semua bahan telah ada di Brew Station
                if (AllIngredientsInBrewStation())
                {
                    // Lakukan proses crafting
                    CraftItem();
                }
                else
                {
                    Debug.Log("Not all ingredients are in Brew Station.");
                }
            }
        }
    }

    public bool AllIngredientsInBrewStation()
    {
        // Mengambil jumlah bahan di Brew Station
        int ingredientCount = brewStation.GetIngredientCount();

        // Implementasi logika pengecekan apakah semua bahan sudah ada di Brew Station
        // Return true jika semua bahan ada, false jika tidak
        // Gunakan logika yang sesuai dengan kebutuhan game Anda
        return ingredientCount >= 2; // Misalnya, return true jika sudah ada minimal 2 bahan
    }

    // Fungsi untuk mengeksekusi proses crafting
    private void CraftItem()
    {
        // Implementasi proses crafting di sini
        Debug.Log("Crafting item");
    }
}
