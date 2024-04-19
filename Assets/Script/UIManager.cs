using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    // Define an event for notifying when a recipe item is purchased
    public delegate void RecipeItemPurchasedDelegate(RecipeItem recipeItem);
    public event RecipeItemPurchasedDelegate OnRecipeItemPurchased;

    private void Awake()
    {
        Instance = this;
    }

    // Method to call when a recipe item is purchased
    public void RecipeItemPurchased(RecipeItem recipeItem)
    {
        OnRecipeItemPurchased?.Invoke(recipeItem);
    }
}
