using UnityEngine;
using System.Collections.Generic;

public class ItemPickup : MonoBehaviour
{
    public Items Item;

    void Pickup()
    {
       InventoryManager.Instance.AddItem(Item);
       Destroy(gameObject);
    }
    
    private void OnMouseDown()
    {
        Pickup();
    }
}
