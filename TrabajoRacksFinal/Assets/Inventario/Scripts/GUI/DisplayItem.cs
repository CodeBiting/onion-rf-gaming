using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static UnityEditor.Progress;



    public class DisplayItem : MonoBehaviour
    {
        [Header("Functionality")]
        [SerializeField]
        private GameEventItem m_Event;

        [Header("Display")]
        [SerializeField]
        private TextMeshProUGUI m_IDText;
        [SerializeField]
        private TextMeshProUGUI m_AmountText;
        [SerializeField]
        private Image m_Image;

        public void Load(ItemIsaac item)
        {
            m_IDText.text = item.Id;
            m_Image.sprite = item.Sprite;
            GetComponent<Button>().onClick.RemoveAllListeners();
            GetComponent<Button>().onClick.AddListener(() => RaiseEvent(item));
        }

        public void Load(Backpack.ItemSlot itemSlot)
        {
            Load(itemSlot.Item);
            m_AmountText.text = itemSlot.Amount.ToString();
        }

        private void RaiseEvent(ItemIsaac item)
        {
            m_Event.Raise(item);
        }
    }