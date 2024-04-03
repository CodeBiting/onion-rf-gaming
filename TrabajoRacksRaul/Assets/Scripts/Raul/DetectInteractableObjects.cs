using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInteractableObjects : MonoBehaviour
{
    //Lógica para detectar objetos interactivos
    //Se usa para almacenar objetos interactivos y poder contarlos
    private List<GameObject> interactableObjects = new List<GameObject>();
private void OnTriggerEnter(Collider other)
    {
        //Si el objeto con el que colisiona es interactivo y no está en la lista de objetos interactivos se añade a la lista
        if (other.gameObject.CompareTag("Interactable") && !interactableObjects.Contains(other.gameObject))
        {
            //Si hay objetos dentro, cambiar el color de la zona a verde pero transparente
            GetComponent<Renderer>().material.color = new Color(0, 1, 0, 0.5f);
            interactableObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Si el objeto con el que deja de colisionar es interactivo y está en la lista de objetos interactivos se elimina de la lista
        if (other.gameObject.CompareTag("Interactable") && interactableObjects.Contains(other.gameObject))
        {
            //Si no hay objetos dentro, quitar el color y dejarlo como antes
            GetComponent<Renderer>().material.color = Color.white;
            interactableObjects.Remove(other.gameObject);
        }
    }

    //Devuelve el número de objetos interactivos que hay en la lista
    public int GetInteractableObjects()
    {
        return interactableObjects.Count;
    }
    
}
