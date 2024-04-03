using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


    public class EfectoVeneno : MonoBehaviour
    {
        public GameObject pl1;
        public GameObject pl2;
        public ScriptableObject veneno;


        public delegate void activarPosicion(float damage);
        public event activarPosicion OnActivar;

        

        public void ActivarVeneno()
        {
            if (player1())
            {
                OnActivar?.Invoke(20);
            }

            if (player2())
            {
                OnActivar?.Invoke(20);
            }

        }

        private bool player1()
        {
            return gameObject == pl1;
        }

        private bool player2()
        {
            return gameObject == pl2;
        }
    }
