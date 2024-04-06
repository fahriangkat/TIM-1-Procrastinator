using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance;

    public List<CraftRecipe> craftingRecipes = new List<CraftRecipe>();
    public Animator potAnimator; // Animator for the Pot object
    public Animator witchAnimator;
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

        foreach (CraftRecipe recipe in craftingRecipes)
        {
            if (CheckRecipeIngredients(recipe))
            {
                matchedRecipe = recipe;
                break;
            }
        }

        if (matchedRecipe != null)
        {
            foreach (Items ingredient in matchedRecipe.requiredIngredients)
            {
                CraftStation.Instance.RemoveItem(ingredient);
            }

            // Trigger animation to start crafting
            potAnimator.SetBool("isCrafting", true); // Atau potAnimator.SetBool("isCrafting", true); tergantung pada setup Animator Anda
            witchAnimator.SetBool("isWitchidle", true);
            StartCoroutine(AddCraftedItemCoroutine(matchedRecipe.resultingItem));
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


    private IEnumerator AddCraftedItemCoroutine(Items craftedItem)
{
    yield return new WaitForSeconds(5f); // Delay selama 5 detik
    
    // Tambahkan item yang dihasilkan ke craft station
    CraftStation.Instance.AddItem(craftedItem);
    
    // Switch back to idle animation after crafting is done
    potAnimator.SetBool("isCrafting", false);
    witchAnimator.SetBool("isWitchidle", false);
}
}
