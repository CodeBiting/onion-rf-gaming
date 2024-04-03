using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioBug : MonoBehaviour
{
    public Vector3 rotacionInicial = new Vector3(0, 180, 0);
    void Start()
    {
        transform.rotation = Quaternion.Euler(rotacionInicial);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
