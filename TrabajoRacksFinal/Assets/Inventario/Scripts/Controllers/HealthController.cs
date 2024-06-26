using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class HealthController : MonoBehaviour, IHealable, IDamageable
    {
        private const float MAXHP = 100f;

        [SerializeField]
        private float m_HP = MAXHP;

        public void Damage(float damageAmount)
        {
            m_HP -= damageAmount;
            if (m_HP < 0)
                m_HP = 0;

            Debug.Log(string.Format("Received {0} damage. Remaining HP: {1}", damageAmount, m_HP));
        }

        public void Heal(float healAmount)
        {
            m_HP += healAmount;
            if (m_HP < 0)
                m_HP = 0;

            Debug.Log(string.Format("Healed by {0} points. Remaining HP: {1}", healAmount, m_HP));
        }
    }
