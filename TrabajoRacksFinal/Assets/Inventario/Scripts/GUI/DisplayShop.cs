using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class DisplayShop : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_DisplayShopParent;

        [SerializeField]
        private GameObject m_DisplayItemPrefab;

        [SerializeField]
        private ItemIsaac[] m_ItemsToDisplay;

        private void Start()
        {
            foreach (ItemIsaac item in m_ItemsToDisplay)
            {
                GameObject displayedItem = Instantiate(m_DisplayItemPrefab, m_DisplayShopParent.transform);
                displayedItem.GetComponent<DisplayItem>().Load(item);
            }
        }
    }

