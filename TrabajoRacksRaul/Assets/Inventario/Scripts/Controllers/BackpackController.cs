using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class BackpackController : MonoBehaviour
    {
        [SerializeField]
        private GameEvent m_GUIEvent;

        [SerializeField]
        private Backpack m_Backpack;

        public void ConsumeItem(ItemIsaac item)
        {
            if (!item.UsedBy(gameObject))
                return;

            m_Backpack.RemoveItem(item);
            m_GUIEvent.Raise();
        }

        public void AddItem(ItemIsaac item)
        {
            m_Backpack.AddItem(item);
            m_GUIEvent.Raise();
        }
    }
