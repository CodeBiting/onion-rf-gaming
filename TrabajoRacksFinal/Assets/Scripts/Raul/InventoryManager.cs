using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    public List<Item> items = new List<Item>();

    public Transform ItemContent;
    public GameObject InventoryItem;
    public GameObject prefabCajas;

    public Toggle EnableRemove;

    public InventoryItemController[] inventoryItems;

    private void Awake()
    {
        instance = this;
    }

    public void AddItem(Item item)
    {
        items.Add(item);

    }

    public void AssignPrefabToItem(Item item, GameObject prefab)
    {
        item.prefab = prefab;
    }

    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    public void ListItems()
    {
        //Clean content before open.
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var ItemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();

            itemName.text = item.itemName;
            ItemIcon.sprite = item.icon;

            Button itemButton = obj.GetComponent<Button>();
            itemButton.onClick.AddListener(() => OnInventoryItemClick(item));

            if (EnableRemove.isOn)
            {
                removeButton.gameObject.SetActive(true);
            }
        }

        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        inventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        for (int i = 0; i < items.Count; i++)
        {
            inventoryItems[i].AddItem(items[i]);
        }
    }

    public void OnInventoryItemClick(Item item)
    {
        // Instanciar una nueva caja (o prefabricado) aquí
        GameObject newBox = Instantiate(prefabCajas); // Asumiendo que cada objeto Item tiene una referencia al prefab de la caja

    }


    public void AddItemToInventory(Item item, GameObject prefab)
    {
        items.Add(item);
        AssignPrefabToItem(item, prefab);
    }
}
