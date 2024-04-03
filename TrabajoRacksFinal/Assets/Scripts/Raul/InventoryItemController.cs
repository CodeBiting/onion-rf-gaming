using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Item item;
    public Button RemoveButton;
   [SerializeField] GameObject box;
    private Transform jugador;

    public void RemoveItem()
    {
        InventoryManager.instance.RemoveItem(item);

        Destroy(gameObject);
    }

    public void AddItem(Item newitem)
    {
    
        item = newitem;
     
    }

    public void AddBox()
    {
        //int contadorCajas = 0;
        jugador = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 spawnPosition = jugador.transform.position + transform.forward * 2.0f;
        //contadorCajas++;
     
        // Instanciar la caja en la posición calculada
        GameObject newBox = Instantiate(box, spawnPosition, Quaternion.identity);
        RemoveItem();
        
            
    }
}
