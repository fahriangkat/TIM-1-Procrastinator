using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Menu",menuName ="Scriptable Object/New Shop Item", order = 1)]

public class ShopItemSO : ScriptableObject

{
    public string itemName;
    public Sprite icon;
    public int price;
}

