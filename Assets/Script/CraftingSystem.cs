using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Items;
using UnityEngine.SceneManagement;

public class CraftingSystem : MonoBehaviour
{
    public static CraftingSystem Instance;

    public List<CraftRecipe> craftingRecipes = new List<CraftRecipe>();
    public Animator potAnimator; // Animator for the Pot object
    public Animator witchAnimator;
    public AudioSource craftSound;
    public AudioClip craftingSoundClip;
    public AudioSource idleSoundSource; // AudioSource untuk suara PotIdle
    public AudioClip idleSoundClip; // Suara PotIdle
    public AudioSource craftedItemSoundSource; // AudioSource untuk sound effect CraftedItem
    public AudioClip craftedItemSoundClip; // Sound effect untuk CraftedItem

    private List<string> allPotions = new List<string>(); // Daftar semua jenis potion dalam game
    private List<string> craftedPotions = new List<string>(); // Daftar potion yang sudah dibuat oleh pemain

    private void Awake()
    {
        Instance = this;
        LoadCraftingRecipes(); // Panggil method untuk memuat resep-resep crafting
        GetAllPotions(); // Panggil method untuk mendapatkan semua jenis potion dalam game
    }

    private void LoadCraftingRecipes()
    {
        // Temukan semua objek CraftRecipe yang ada di proyek
        CraftRecipe[] recipes = Resources.LoadAll<CraftRecipe>("CraftingRecipes");

        // Tambahkan resep-resep crafting ke dalam list craftingRecipes
        craftingRecipes.AddRange(recipes);
    }

    // Method untuk mendapatkan semua jenis potion dalam game
    private void GetAllPotions()
{
    foreach (CraftRecipe recipe in craftingRecipes)
    {
        // Jika item yang dihasilkan oleh resep adalah potion, tambahkan nama potion ke dalam list allPotions
        // Perhatikan bahwa ini telah diperbarui untuk memeriksa kelas Items, bukan PotionItem
        if (recipe.resultingItem is Items)
        {
            allPotions.Add(recipe.resultingItem.itemName);
        }
    }
}

// Kemudi

    // Method untuk menyalakan suara PotIdle
    private void PlayIdleSound()
    {
        if (!idleSoundSource.isPlaying) // Memastikan suara tidak diputar berulang-ulang
        {
            idleSoundSource.clip = idleSoundClip;
            idleSoundSource.loop = true; // Aktifkan looping agar suara tetap berbunyi
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

        // Cek apakah resep sudah terbuka
        if (!recipe.isUnlocked)
        {
            Debug.Log("Recipe is locked. Cannot craft.");
            return false;
        }

        // Iterasi semua bahan-bahan yang diperlukan oleh resep
        foreach (Items ingredient in recipe.requiredIngredients)
        {
            // Cek apakah bahan tersedia di craft station
            if (!CraftStation.Instance.HasIngredient(ingredient))
            {
                Debug.Log("Missing ingredient: " + ingredient.itemName);
                return false;
            }
        }

        // Jika semua bahan tersedia dan resep terbuka, kembalikan true
        return true;
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

    public void CraftItem(CraftRecipe recipe)
    {
        if (CanCraft(recipe))
        {
            // Temukan resep yang sesuai dengan bahan-bahan di craft station
            CraftRecipe matchedRecipe = null;
            foreach (CraftRecipe r in craftingRecipes)
            {
                if (r == recipe)
                {
                    matchedRecipe = r;
                    break;
                }
            }

            if (matchedRecipe != null)
            {
                // Hapus bahan-bahan yang digunakan untuk crafting dari craft station
                foreach (Items ingredient in matchedRecipe.requiredIngredients)
                {
                    CraftStation.Instance.RemoveItem(ingredient);
                }

                // Jalankan proses crafting
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

    // CraftingSystem.cs
    public CraftRecipe GetMatchedRecipe()
    {
        foreach (CraftRecipe recipe in craftingRecipes)
        {
            // Cek apakah resep cocok dan telah terbuka
            if (CheckRecipeIngredients(recipe) && recipe.isUnlocked)
            {
                return recipe;
            }
        }
        return null;
    }

    public void UnlockRecipe(CraftRecipe recipe)
    {
        // Temukan resep yang sesuai di dalam daftar craftingRecipes
        CraftRecipe matchedRecipe = craftingRecipes.Find(r => r == recipe);
        
        if (matchedRecipe != null)
        {
            matchedRecipe.isUnlocked = true; // Setel status resep menjadi terbuka
            Debug.Log("Recipe unlocked: " + matchedRecipe.recipeName);
        }
        else
        {
            Debug.LogWarning("Recipe not found: " + recipe.recipeName);
        }
    }

    public bool IsRecipeUnlocked(CraftRecipe recipe)
    {
        // Temukan resep yang sesuai di dalam daftar craftingRecipes
        CraftRecipe matchedRecipe = craftingRecipes.Find(r => r == recipe);
        
        if (matchedRecipe != null)
        {
            return matchedRecipe.isUnlocked; // Kembalikan status terkunci atau terbuka dari resep
        }
        else
        {
            Debug.LogWarning("Recipe not found: " + recipe.recipeName);
            return false;
        }
    }

  private IEnumerator AddCraftedItemCoroutine(Items craftedItem)
{
    yield return new WaitForSeconds(5f); // Delay selama 5 detik
        
    // Tambahkan item yang dihasilkan ke craft station
    CraftStation.Instance.AddItem(craftedItem);
        
    // Trigger sound effect untuk CraftedItem
    craftedItemSoundSource.PlayOneShot(craftedItemSoundClip);

    // Switch back to idle animation after crafting is done
    potAnimator.SetBool("isCrafting", false);
    witchAnimator.SetBool("isWitchidle", false);

    // Matikan suara PotIdle setelah item selesai di craft

    // Tambahkan pemanggilan ke method AddCraftedPotion dari PotionManager
    if (craftedItem is Items)
    {
        // Ubah Items menjadi Items jika Items adalah turunan dari Items
        Items item = craftedItem as Items;
        // Jika diperlukan, tambahkan logika untuk menangani item yang berbeda di sini
    }

    // Periksa apakah semua jenis potion telah dibuat
    if (IsAllPotionsCrafted())
    {
        // Panggil method untuk memicu pergantian scene ke end screen
        GoToEndScreen();
    }
}

    // Method untuk memeriksa apakah pemain telah membuat semua jenis potion yang ada di game
    private bool IsAllPotionsCrafted()
    {
        // Periksa apakah semua jenis potion sudah dibuat
        foreach (string potionName in allPotions)
        {
            if (!craftedPotions.Contains(potionName))
            {
                return false; // Belum semua potion dibuat
            }
        }
        return true; // Semua potion sudah dibuat
    }

    // Method untuk memicu pergantian scene ke end screen
    private void GoToEndScreen()
    {
        SceneManager.LoadScene(3); // Ganti angka 1 dengan nomor indeks scene end screen yang kamu miliki
    }
}
