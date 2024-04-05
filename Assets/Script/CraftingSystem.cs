using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance;

    public List<CraftRecipe> craftingRecipes = new List<CraftRecipe>();

    private void Awake()
    {
        Instance = this;
        LoadCraftingRecipes(); // Panggil method untuk memuat resep-resep crafting
    }

    private void LoadCraftingRecipes()
    {
        // Temukan semua objek CraftRecipe yang ada di proyek
        CraftRecipe[] recipes = Resources.LoadAll<CraftRecipe>("CraftingRecipes");

        // Tambahkan resep-resep crafting ke dalam list craftingRecipes
        craftingRecipes.AddRange(recipes);
    }

    public bool CanCraft()
    {
        if (CraftStation.Instance == null)
        {
            Debug.LogError("CraftStation is not initialized.");
            return false;
        }

        // Iterasi semua resep crafting
        foreach (CraftRecipe recipe in craftingRecipes)
        {
            // Cek apakah bahan-bahan di craft station sesuai dengan resep saat ini
            if (CheckRecipeIngredients(recipe))
            {
                // Jika sesuai, kembalikan true
                return true;
            }
        }

        // Jika tidak ada resep yang sesuai dengan bahan-bahan di craft station, kembalikan false
        return false;
    }

    private bool CheckRecipeIngredients(CraftRecipe recipe)
    {
        // Iterasi semua bahan yang diperlukan oleh resep
        foreach (Items ingredient in recipe.requiredIngredients)
        {
            // Jika bahan tidak ada di craft station, resep tidak bisa diproses
            if (!CraftStation.Instance.HasIngredient(ingredient))
            {
                return false;
            }
        }

        // Jika semua bahan ditemukan di craft station, resep bisa diproses
        return true;
    }

    public void CraftItem()
    {
        if (CanCraft())
        {
            CraftRecipe matchedRecipe = null;

            // Iterasi semua resep crafting
            foreach (CraftRecipe recipe in craftingRecipes)
            {
                // Cek apakah bahan-bahan di craft station sesuai dengan resep saat ini
                if (CheckRecipeIngredients(recipe))
                {
                    // Jika sesuai, set matchedRecipe ke resep saat ini
                    matchedRecipe = recipe;
                    break; // Keluar dari loop karena sudah menemukan resep yang cocok
                }
            }

            // Jika ada resep yang cocok ditemukan
            if (matchedRecipe != null)
            {
                // Hapus semua bahan yang digunakan dari craft station
                foreach (Items ingredient in matchedRecipe.requiredIngredients)
                {
                    CraftStation.Instance.RemoveItem(ingredient);
                }
                // Tambahkan item yang dihasilkan ke craft station
                CraftStation.Instance.AddItem(matchedRecipe.resultingItem);
            }
            else
            {
                Debug.Log("No matching recipe found for current ingredients.");
            }
        }
        else
        {
            Debug.Log("Not enough ingredients to craft.");
        }
    }
}
