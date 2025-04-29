using System;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.PersistentProgress
{
    [Serializable]
    public class ItemSaveData
    {
        public string id;
        public int durability;
        public Vector3 position;
        public Quaternion rotation;

        public ItemSaveData(string id, int durability, Vector3 position, Quaternion rotation)
        {
            this.id = id;
            this.durability = durability;
            this.position = position;
            this.rotation = rotation;
        }
    }
}