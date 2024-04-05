using UnityEngine;
using System.Collections.Generic;

public class BrewStation : MonoBehaviour
{
    public static BrewStation Instance;

    // List of ingredients currently in the brew station
    private List<Ingredient> ingredients = new List<Ingredient>();

    // List of items currently in the brew station (crafted items)
    private List<Items> items = new List<Items>();

    private void Awake()
    {
        Instance = this;
    }

    public bool HasIngredient(Ingredient ingredient)
    {
        return ingredients.Contains(ingredient);
    }

    public bool HasItem(Items item)
    {
        return items.Contains(item);
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);
    }

    public void AddItem(Items item)
    {
        items.Add(item);
    }

    public void RemoveIngredient(Ingredient ingredient)
    {
        ingredients.Remove(ingredient);
    }

    public void RemoveItem(Items item)
    {
        items.Remove(item);
    }

    public int GetIngredientCount()
    {
        return ingredients.Count;
    }
    // Add other methods and properties as needed
}
