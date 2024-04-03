using System;
using UnityEngine;



    public interface IItem
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public Sprite Sprite { get; set; }

        public bool UsedBy(GameObject go);
    }