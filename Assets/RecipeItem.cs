using UnityEngine;

[CreateAssetMenu(fileName = "New RecipeItem", menuName = "Items/Create New RecipeItem")]
public class RecipeItem : Items // Inherit from Items class
{
    public CraftRecipe correspondingRecipe; // Property to associate with the corresponding recipe
}
