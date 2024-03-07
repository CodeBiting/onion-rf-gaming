/*using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

[System.Serializable]
public class RackList
{
    public List<RackData> racks;
}

[System.Serializable]
public class RackData
{
    public int id;
    public string code;
    public int typeId;
    public string placeTypeCode;
    public float x;
    public float y;
    public float z;
    public string subtypeId;
    public int allowStock;
    public int parentId;
    public Position position;
    public Rotation rotation;
    public Scale scale;
    public List<Shelf> shelfs;
}

[System.Serializable]
public class Shelf
{
    public float y;
}

[System.Serializable]
public class Position
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class Rotation
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class Scale
{
    public float x;
    public float y;
    public float z;
}

[System.Serializable]
public class WarehouseResponse
{
    public List<RackData> warehouse5x2;
}

public class WarehouseRequest : MonoBehaviour
{
    [SerializeField]
    private GameObject cubos;
    private RackList rackList;
    // URL del servidor
    private string url = "http://localhost:3000/warehouse5x2s"; // URL

    IEnumerator Start()
    {
        // Crear una instancia de UnityWebRequest para la solicitud GET
        UnityWebRequest www = UnityWebRequest.Get(url);

        // Enviar la solicitud y esperar la respuesta
        yield return www.SendWebRequest();

        // Verificar si hubo errores
        if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            Debug.Log(jsonString);
            WarehouseResponse response = JsonUtility.FromJson<WarehouseResponse>(jsonString);

            rackList = new RackList { racks = response.warehouse5x2 };

            foreach (RackData place in rackList.racks)
            {
                Debug.Log("ID: " + place.id);
                Debug.Log("Code: " + place.code);
                Debug.Log("Type ID: " + place.typeId);
                Debug.Log("Place Type Code: " + place.placeTypeCode);
                Debug.Log("X: " + place.x);
                Debug.Log("Y: " + place.y);
                Debug.Log("Z: " + place.z);
                Debug.Log("Subtype ID: " + place.subtypeId);
                Debug.Log("Allow Stock: " + place.allowStock);
                Debug.Log("Parent ID: " + place.parentId);
                Debug.Log("Position X: " + place.position.x);
                Debug.Log("Position Y: " + place.position.y);
                Debug.Log("Position Z: " + place.position.z);
                Debug.Log("Rotation X: " + place.rotation.x);
                Debug.Log("Rotation Y: " + place.rotation.y);
                Debug.Log("Rotation Z: " + place.rotation.z);
                Debug.Log("Scale X: " + place.scale.x);
                Debug.Log("Scale Y: " + place.scale.y);
                Debug.Log("Scale Z: " + place.scale.z);

                Debug.Log("--------------------");
                Vector3 posicion = new Vector3(place.position.x, place.position.y, place.position.z);

                Debug.Log("Asignando rotacion:");
                Quaternion rotacion = Quaternion.Euler(place.rotation.x, place.rotation.y, place.rotation.z);

                Debug.Log("Asignando escala:");
                Vector3 escala = new Vector3(place.scale.x, place.scale.y, place.scale.z);

                Debug.Log("Instanciar objeto:");
                GameObject instanciaCubo = Instantiate(cubos, posicion, rotacion);
                instanciaCubo.transform.localScale = escala;
                Debug.Log("CUBO CREADO.");
            }
        }
    }
}
*/