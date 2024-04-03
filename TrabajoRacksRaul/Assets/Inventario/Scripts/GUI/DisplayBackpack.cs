using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DisplayBackpack : MonoBehaviour
{
    [SerializeField]
    private GameObject m_DisplayBackpackParent;

    [SerializeField]
    private GameObject m_DisplayItemPrefab;

    [SerializeField]
    private Backpack m_Backpack;

    private Dictionary<string, GameObject> m_PrefabDictionary;

    [System.Serializable]
    public struct ItemPrefabPair
    {
        public string itemId;
        public GameObject itemPrefab;
    }

    [SerializeField]
    private List<ItemPrefabPair> m_ItemPrefabPairs;

    private void Start()
    {
        FillDisplay();
    }

    private void ClearDisplay()
    {
        foreach (Transform child in m_DisplayBackpackParent.transform)
            Destroy(child.gameObject);
    }

    private void FillDisplay()
    {
        foreach (Backpack.ItemSlot itemSlot in m_Backpack.ItemSlots)
        {
            GameObject displayedItem = Instantiate(m_DisplayItemPrefab, m_DisplayBackpackParent.transform);
            displayedItem.GetComponent<DisplayItem>().Load(itemSlot);
        }
    }

    public void RefreshBackpack()
    {
        ClearDisplay();
        FillDisplay();
    }

}
    // Llamar FillDisplay() cada vez que se actualice el inventario
    /*
    private void Start()
    {
        InitializePrefabDictionary();
        RefreshBackpack();
    }

    private void InitializePrefabDictionary()
    {
        m_PrefabDictionary = new Dictionary<string, GameObject>();
        if (m_ItemPrefabPairs != null)
        {
            foreach (var pair in m_ItemPrefabPairs)
            {
                m_PrefabDictionary[pair.itemId] = pair.itemPrefab;
            }
        }
        else
        {
            Debug.LogWarning("La lista de pares de prefabs es nula.");
        }
    }

    private void ClearDisplay()
    {
        foreach (Transform child in m_DisplayBackpackParent.transform)
        {
           // Destroy(child.gameObject);
        }
    }

    private void FillDisplay()
    {
        foreach (Backpack.ItemSlot itemSlot in m_Backpack.ItemSlots)
        {
            if (m_PrefabDictionary.ContainsKey(itemSlot.Item.Id))
            {
                GameObject displayedItem = Instantiate(m_PrefabDictionary[itemSlot.Item.Id], m_DisplayBackpackParent.transform);
                displayedItem.GetComponent<DisplayItem>().Load(itemSlot);
            }
            else
            {
                Debug.LogWarning("No se encontró prefab para el objeto con ID: " + itemSlot.Item.Id);
            }
        }
    }

    public void RefreshBackpack()
    {
        ClearDisplay();
        FillDisplay();
    }
}*/