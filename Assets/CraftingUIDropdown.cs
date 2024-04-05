using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CraftingUIDropdown : MonoBehaviour
{
    public Dropdown dropdown;
    public List<CraftRecipe> recipes;

    void Start()
    {
        dropdown.ClearOptions();

        var options = new List<Dropdown.OptionData>();

        foreach (var recipe in recipes)
        {
            // Menggunakan properti recipeName
            options.Add(new Dropdown.OptionData(recipe.recipeName));
        }

        dropdown.AddOptions(options);
    } 
}
