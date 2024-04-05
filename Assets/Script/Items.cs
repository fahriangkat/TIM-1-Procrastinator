using UnityEngine;

[CreateAssetMenu(fileName = "New Item",menuName ="Item/Create New Item")]
public class Items : ScriptableObject
{
    // Start is called before the first frame update
    public string itemName;
    public Sprite icon;
    public int price;
}
