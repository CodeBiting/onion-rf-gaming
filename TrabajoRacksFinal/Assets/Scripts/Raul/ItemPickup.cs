using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    void Pickup()
    {
        Debug.Log("Picking up " + item.name);
        InventoryManager.instance.AddItem(item);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Pickup();
    }
    
}
