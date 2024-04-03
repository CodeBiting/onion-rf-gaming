using UnityEngine;

public class JsonPrefabEditor : MonoBehaviour
{
    public GameObject prefabParent;
    public GameObject childObject;

    private void Start()
    {
        string jsonFilePath = "spawnracks.json";

        // Leer el archivo JSON y obtener los datos
        string jsonText = System.IO.File.ReadAllText(jsonFilePath);
        JsonData data = JsonUtility.FromJson<JsonData>(jsonText);

        // Editar el tamaño del prefab padre
        Vector3 newSize = new Vector3(data.prefabSize.x, data.prefabSize.y, data.prefabSize.z);
        prefabParent.transform.localScale = newSize;

        // Editar la altura del hijo
        Vector3 newChildSize = childObject.transform.localScale;
        newChildSize.y = data.childHeight;
        childObject.transform.localScale = newChildSize;
    }
}

[System.Serializable]
public class JsonData
{
    public Vector3Int prefabSize;
    public float childHeight;
}
