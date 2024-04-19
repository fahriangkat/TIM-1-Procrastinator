using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionManager : MonoBehaviour
{
    public static PotionManager Instance;

    public List<string> craftedPotions = new List<string>();

    private void Awake()
    {
        Instance = this;
    }

    public void AddCraftedPotion(string potionName)
    {
        craftedPotions.Add(potionName);
        Debug.Log("Crafted potion: " + potionName);
    }
}
