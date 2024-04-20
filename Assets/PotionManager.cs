using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public static PotionManager Instance;

    public List<string> craftedPotions = new List<string>(); // Daftar nama potion yang telah dibuat

    private void Awake()
    {
        Instance = this;
    }

    public void AddCraftedRecipe(CraftRecipe recipe)
    {
        // Tambahkan nama potion yang dihasilkan oleh resep ke daftar craftedPotions
        craftedPotions.Add(recipe.resultingItem.itemName);

        // Tambahkan debug log untuk memeriksa daftar potion yang telah dibuat
        Debug.Log("Crafted Potions:");
        foreach (string potion in craftedPotions)
        {
            Debug.Log("- " + potion);
        }
    }
}
