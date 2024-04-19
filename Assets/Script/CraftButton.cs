using UnityEngine;

public class CraftButton : MonoBehaviour
{
    //public CraftingSystem craftingSystem;
    //public CraftRecipe recipe; // Menambahkan variabel recipe

    public void OnCraftButtonClicked()
    {
        CraftingSystem craftingSystem = CraftingSystem.Instance; // Mengambil referensi ke instance CraftingSystem
        if (craftingSystem != null)
        {
            // Tidak perlu lagi memeriksa recipe pada CraftButton, karena proses cek recipe dilakukan di dalam CraftingSystem
            // if (recipe != null) // Memeriksa apakah recipe telah diberikan nilai
            // {
                // Jika bahan cukup, lakukan proses crafting
                // if (CraftingSystem.Instance.CanCraft(recipe))
                CraftRecipe matchedRecipe = craftingSystem.GetMatchedRecipe(); // Mendapatkan resep yang sesuai
                if (matchedRecipe != null)
                {
                    craftingSystem.CraftItem(matchedRecipe); // Melakukan crafting menggunakan resep yang sesuai
                }
                else
                {
                    Debug.Log("No recipe available for crafting.");
                }
            // }
            // else
            // {
            //     Debug.Log("No recipe selected.");
            // }
        }
        else
        {
            Debug.LogError("Crafting system not found.");
        }
    }
}
