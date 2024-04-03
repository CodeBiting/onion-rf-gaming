using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Item item;
    public Button RemoveButton;

    public void RemoveItem()
    {
        InventoryManager.instance.RemoveItem(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newitem)
    {
        item = newitem;
    }
}
