using UnityEngine;

public class SellButton : MonoBehaviour
{
    private Items item; // Item yang akan dijual

    public void SetItem(Items newItem)
    {
        item = newItem;
    }

    public void SellItem()
    {
        if (item != null)
        {
            Debug.Log("Sell button clicked for item: " + item.itemName);
            // Implement sell item logic here
        }
        else
        {
            Debug.LogWarning("Item is null.");
        }
    }
}
