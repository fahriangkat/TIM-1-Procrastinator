using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance;

    public List<CraftRecipe> craftingRecipes = new List<CraftRecipe>();
    public Animator potAnimator;
    public Animator witchAnimator;
    public AudioSource craftSound;
    public AudioClip craftingSoundClip;
    public AudioSource idleSoundSource;
    public AudioClip idleSoundClip;
    public AudioSource craftedItemSoundSource;
    public AudioClip craftedItemSoundClip;

    private List<string> craftedPotions = new List<string>(); // Daftar nama potion yang telah dibuat

    private void Awake()
    {
        Instance = this;
        LoadCraftingRecipes();
    }

    private void LoadCraftingRecipes()
    {
        CraftRecipe[] recipes = Resources.LoadAll<CraftRecipe>("CraftingRecipes");
        craftingRecipes.AddRange(recipes);
    }

    private void PlayIdleSound()
    {
        if (!idleSoundSource.isPlaying)
        {
            idleSoundSource.clip = idleSoundClip;
            idleSoundSource.loop = true;
            idleSoundSource.Play();
        }
    }

    public bool CanCraft(CraftRecipe recipe)
    {
        if (CraftStation.Instance == null)
        {
            Debug.LogError("CraftStation is not initialized.");
            return false;
        }

        if (!recipe.isUnlocked)
        {
            Debug.Log("Recipe is locked. Cannot craft.");
            return false;
        }

        foreach (Items ingredient in recipe.requiredIngredients)
        {
            if (!CraftStation.Instance.HasIngredient(ingredient))
            {
                Debug.Log("Missing ingredient: " + ingredient.itemName);
                return false;
            }
        }

        return true;
    }

    private bool CheckRecipeIngredients(CraftRecipe recipe)
    {
        foreach (Items ingredient in recipe.requiredIngredients)
        {
            if (!CraftStation.Instance.HasIngredient(ingredient))
            {
                return false;
            }
        }
        return true;
    }

    public void CraftItem(CraftRecipe recipe)
    {
        if (CanCraft(recipe))
        {
            CraftRecipe matchedRecipe = craftingRecipes.FirstOrDefault(r => r == recipe);

            if (matchedRecipe != null)
            {
                foreach (Items ingredient in matchedRecipe.requiredIngredients)
                {
                    CraftStation.Instance.RemoveItem(ingredient);
                }

                potAnimator.SetBool("isCrafting", true);
                witchAnimator.SetBool("isWitchidle", true);
                craftSound.PlayOneShot(craftingSoundClip);
                StartCoroutine(AddCraftedItemCoroutine(matchedRecipe.resultingItem));
            }
            else
            {
                Debug.Log("No matching recipe found for current ingredients.");
            }
        }
        else
        {
            Debug.Log("Cannot craft item. Recipe is locked or missing ingredients.");
        }
    }

    public CraftRecipe GetMatchedRecipe()
    {
        foreach (CraftRecipe recipe in craftingRecipes)
        {
            if (CheckRecipeIngredients(recipe) && recipe.isUnlocked)
            {
                return recipe;
            }
        }
        return null;
    }

    public void UnlockRecipe(CraftRecipe recipe)
    {
        CraftRecipe matchedRecipe = craftingRecipes.Find(r => r == recipe);
        
        if (matchedRecipe != null)
        {
            matchedRecipe.isUnlocked = true;
            Debug.Log("Recipe unlocked: " + matchedRecipe.recipeName);
        }
        else
        {
            Debug.LogWarning("Recipe not found: " + recipe.recipeName);
        }
    }

    public bool IsRecipeUnlocked(CraftRecipe recipe)
    {
        CraftRecipe matchedRecipe = craftingRecipes.Find(r => r == recipe);
        
        if (matchedRecipe != null)
        {
            return matchedRecipe.isUnlocked;
        }
        else
        {
            Debug.LogWarning("Recipe not found: " + recipe.recipeName);
            return false;
        }
    }

    private IEnumerator AddCraftedItemCoroutine(Items craftedItem)
    {
        yield return new WaitForSeconds(5f);
        
        CraftStation.Instance.AddItem(craftedItem);
        
        craftedItemSoundSource.PlayOneShot(craftedItemSoundClip);

        potAnimator.SetBool("isCrafting", false);
        witchAnimator.SetBool("isWitchidle", false);

        // Tambahkan nama potion ke daftar craftedPotions
        craftedPotions.Add(craftedItem.itemName);

        // Periksa apakah semua resep telah dibuat
        if (IsAllPotionsCrafted())
        {
            GoToEndScreen();
        }
    }

    private bool IsAllPotionsCrafted()
    {
        // Periksa apakah semua resep telah dibuat
        foreach (CraftRecipe recipe in craftingRecipes)
        {
            if (!craftedPotions.Contains(recipe.resultingItem.itemName))
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator GoToEndScreenAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    SceneManager.LoadScene(3);
}

private void GoToEndScreen()
{
    StartCoroutine(GoToEndScreenAfterDelay(5f)); // Menunggu 5 detik sebelum berpindah ke scene berikutnya
}
}
