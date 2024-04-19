using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Create New Item")]
public class Items : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int price;
    public int sellPrice;
    public bool isSelected; // Flag to indicate if the item is selected
}
