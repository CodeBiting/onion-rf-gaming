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
    public bool isPartOfOrder;
    public GameObject gameObject;

    public Caixes(GameObject gameObject, float x, float z, Scale scaleCaixes)
    {
        this.x = x;
        this.z = z;
        this.scaleCaixes = scaleCaixes;
    }
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
    private List<Caixes> allCaixes = new List<Caixes>(); // Lista para almacenar todas las cajas

    // URL del servidor
    private string url = "http://localhost:3000/warehouse5x2s"; // URL

    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

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
                Quaternion rotacion = Quaternion.Euler(place.rotation.x - 90f, place.rotation.y, place.rotation.z);
                Vector3 escala = new Vector3(place.scale.x, place.scale.y, place.scale.z);

                GameObject instanciaCubo = Instantiate(cubos, posicion, rotacion);
                Destroy(instanciaCubo.transform.GetChild(0).gameObject);
                instanciaCubo.transform.localScale = escala;

                foreach (Shelf shelf in place.shelfs)
                {
                    float shelfHeight = shelf.y;

                    GameObject newChild = Instantiate(hijoPrefab, instanciaCubo.transform);
                    newChild.transform.Rotate(90f, 0f, 0f);
                    Vector3 childPosition = new Vector3(0f, 0f, shelfHeight);
                    newChild.transform.localPosition = childPosition;

                    foreach (Caixes caixes in shelf.caixes)
                    {
                        Vector3 posicionCaixa = new Vector3(caixes.x, caixes.z, (caixes.scaleCaixes.z * 0.546f));
                        GameObject instanciaCaixa = Instantiate(caixaPrefab, newChild.transform);
                        Caixes caixesObject = new Caixes(instanciaCaixa, caixes.x, caixes.z, caixes.scaleCaixes); // Corregir aquí
                        caixesObject.gameObject = instanciaCaixa; // Asignar gameObject después de la instancia
                        instanciaCaixa.transform.localPosition = posicionCaixa;
                        instanciaCaixa.transform.localScale = new Vector3(caixes.scaleCaixes.x, caixes.scaleCaixes.y, caixes.scaleCaixes.z);
                    }

                }
            }
        }
        GenerarComandasAleatorias();
    }

    void GenerarComandasAleatorias()
    {
        if (rackList == null || rackList.racks == null || rackList.racks.Count == 0)
        {
            Debug.LogError("No hay estanterías en la lista.");
            return;
        }

        foreach (RackData rack in rackList.racks)
        {
            if (rack.shelfs == null || rack.shelfs.Count == 0)
            {
                Debug.LogWarning($"No hay estanterías en el rack {rack.code}.");
                continue;
            }

            foreach (Shelf estanteriaShelf in rack.shelfs)
            {
                if (estanteriaShelf.caixes == null || estanteriaShelf.caixes.Count == 0)
                {
                    Debug.LogWarning($"No hay cajas en la estantería {rack.code}-{estanteriaShelf.y}.");
                    continue;
                }

                foreach (Caixes caja in estanteriaShelf.caixes)
                {
                    // Generar aleatoriamente si esta caja debe ser parte del pedido
                    caja.isPartOfOrder = Random.value > 0.5f; // 50% de probabilidad de ser verdadero
                    allCaixes.Add(caja); // Agregar cada caja a la lista de todas las cajas
                    Debug.Log($"La caja en la estantería {rack.code}-{estanteriaShelf.y} es parte del pedido: {caja.isPartOfOrder}");
                }
            }
        }

        CambiarColorCajasPedidos();
    }




    void CambiarColorCajasPedidos()
    {
        for (int i = 0; i < allCaixes.Count; i++)
        {
            Caixes caja = allCaixes[i];
            if (caja.isPartOfOrder&& caja.gameObject != null)
            {
                // Change material to cajaPedido
                caja.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
        }


        /*foreach (Caixes caja in allCaixes)
        {
            if (caja.isPartOfOrder)
            {
                Debug.Log($"La caja en la posición ({caja.x}, {caja.z}) es parte del pedido.");
                // Verifica si el objeto de la caja tiene un componente Renderer
                Renderer renderer = caja.gameObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Debug.Log("La caja tiene un componente Renderer adjunto.");
                    // Obtiene el material actual del objeto de la caja
                    Material material = renderer.material;

                    // Cambia el color del material
                    material.color = Color.red;
                }
                else
                {
                    Debug.Log("La caja no tiene un componente Renderer adjunto.");
                }
            }
        }*/
    }
}
