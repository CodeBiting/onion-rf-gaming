using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "Healing Item", menuName = "Inventory/Items/Healing")]
    public class HealingItem : ItemIsaac
    {
        [Header("Healing Item values")]
        [SerializeField]
        [Min(0f)]
        private float m_Healing;

        public override bool UsedBy(GameObject go)
        {
            if (!go.TryGetComponent(out IHealable healable))
                return false;

            healable.Heal(m_Healing);
            return true;
        }

       
    }

    
