// CraftButton.cs
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    public CraftingSystem craftingSystem;

    public void OnCraftButtonClicked()
    {
        if (craftingSystem != null)
        {
            if (craftingSystem.craftingRecipes.Count > 0)
            {
                // Jika bahan cukup, lakukan proses crafting
                if (craftingSystem.CanCraft())
                {
                    craftingSystem.CraftItem();
                }
                else
                {
                    Debug.Log("Not enough ingredients to craft.");
                }
            }
            else
            {
                Debug.Log("No crafting recipes available.");
            }
        }
        else
        {
            Debug.LogError("Crafting system not found.");
        }
    }
}
