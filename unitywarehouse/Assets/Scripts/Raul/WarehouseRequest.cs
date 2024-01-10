using JetBrains.Annotations;
using System.Collections;
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
    public List<Caixes> caixes;
}

[System.Serializable]
public class Caixes
{
    public float x;
    public float z;
    public Scale scaleCaixes;
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
    public List<RackData> warehouse5x2s;
}

public class WarehouseRequest : MonoBehaviour
{
    [SerializeField]
    private GameObject cubos;
    private RackList rackList;
    [SerializeField]
    private GameObject hijoPrefab;
    [SerializeField] private GameObject caixaPrefab;

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
            rackList = new RackList { racks = response.warehouse5x2s };

            foreach (RackData place in rackList.racks)
            {
                Vector3 posicion = new Vector3(place.position.x, (place.scale.z * 1.68f), place.position.z);
                Quaternion rotacion = Quaternion.Euler(place.rotation.x - 90f, place.rotation.y, place.rotation.z); // Ajuste de rotación

                Vector3 escala = new Vector3(place.scale.x, place.scale.y, place.scale.z);

                GameObject instanciaCubo = Instantiate(cubos, posicion, rotacion);
                Destroy(instanciaCubo.transform.GetChild(0).gameObject);
                instanciaCubo.transform.localScale = escala;


                foreach (Shelf shelf in place.shelfs)
                {
                    float shelfHeight = shelf.y;


                    // Crea un nuevo hijo a partir del prefab hijoPrefab
                    GameObject newChild = Instantiate(hijoPrefab, instanciaCubo.transform);

                    // Aplicar rotación al objeto hijo antes de posicionarlo
                    newChild.transform.Rotate(90f, 0f, 0f);

                    // Ajusta la posición vertical del hijo en relación con el padre
                    Vector3 childPosition = new Vector3(0f, 0f, shelfHeight);
                    newChild.transform.localPosition = childPosition;

                    foreach (Caixes caixes in shelf.caixes)
                    {
                        Vector3 posicionCaixa = new Vector3(caixes.x, caixes.z, (caixes.scaleCaixes.z*0.546f));
                        GameObject instanciaCaixa = Instantiate(caixaPrefab, newChild.transform);
                        instanciaCaixa.transform.localPosition = posicionCaixa;

                        instanciaCaixa.transform.localScale = new Vector3(caixes.scaleCaixes.x, caixes.scaleCaixes.y, caixes.scaleCaixes.z);
                    }

                }

            }
        }
    }
}
