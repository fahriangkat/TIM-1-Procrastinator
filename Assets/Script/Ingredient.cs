using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Crafting/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public Sprite icon;
    // Other properties as needed
}

