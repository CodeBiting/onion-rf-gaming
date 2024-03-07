using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activarInventario : MonoBehaviour
{
    public void ActivarLimon()
    {
        Debug.Log("Juan");
        InventoryManager.instance.ListItems();
        this.gameObject.SetActive(true);
        
    }

}
