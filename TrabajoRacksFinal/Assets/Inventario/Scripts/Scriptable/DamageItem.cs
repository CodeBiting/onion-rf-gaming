using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu(fileName = "Damaging Item", menuName = "Inventory/Items/Damaging")]
    public class DamageItem : ItemIsaac
    {
        [Header("Damaging Item values")]
        [SerializeField]
        [Min(0f)]
        private float m_Damage;

        public override bool UsedBy(GameObject go)
        {
            if (!go.TryGetComponent(out IDamageable damageable))
                return false;

            damageable.Damage(m_Damage);
            return true;
        }
    }

