using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System;
using System.Linq;




    [CreateAssetMenu(fileName = "Backpack", menuName = "Inventory/Backpack")]
    public class Backpack : ScriptableObject
    {
        [Serializable]
        public class ItemSlot
        {
            [SerializeField]
            public ItemIsaac Item;
            [SerializeField]
            public int Amount;

            public ItemSlot(ItemIsaac obj)
            {
                Item = obj;
                Amount = 1;
            }
        }

        [SerializeField]
        private List<ItemSlot> m_ItemSlots = new List<ItemSlot>();
        public ReadOnlyCollection<ItemSlot> ItemSlots => new ReadOnlyCollection<ItemSlot>(m_ItemSlots);

        public void AddItem(ItemIsaac usedItem)
        {
            ItemSlot item = GetItem(usedItem);
            if (item == null)
                m_ItemSlots.Add(new ItemSlot(usedItem));
            else
                item.Amount++;
        }

        public void RemoveItem(ItemIsaac usedItem)
        {
            ItemSlot item = GetItem(usedItem);
            if (item == null)
                return;

            item.Amount--;
            if (item.Amount <= 0)
                m_ItemSlots.Remove(item);
        }

        private ItemSlot GetItem(ItemIsaac item)
        {
            return m_ItemSlots.FirstOrDefault(slot => slot.Item == item);
        }
    }


