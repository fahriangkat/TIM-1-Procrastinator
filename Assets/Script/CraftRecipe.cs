using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class CraftRecipe : ScriptableObject
{
    public string recipeName; // Menambahkan properti recipeName
    public List<Items> requiredIngredients; // Menggunakan tipe data 'Items' untuk daftar bahan yang diperlukan
    public Items resultingItem; // Menggunakan tipe data 'Items' untuk item yang dihasilkan
    public bool isUnlocked; //Properti untuk menandai apakah resep telah terbuka atau belum
}
