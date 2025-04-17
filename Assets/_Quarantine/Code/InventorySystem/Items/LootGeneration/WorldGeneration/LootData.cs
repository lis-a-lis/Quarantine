using System;
using UnityEngine;

namespace _Quarantine.Code.InventorySystem.Items.LootGeneration.WorldGeneration
{
    [Serializable]
    public class LootData
    {
        [SerializeField] [ItemIDSelector] private string _itemID;
        [SerializeField] [Range(0, 1)] private float _spawnChance = 1f;
        
        public string ItemID => _itemID;
        
        public float SpawnChance => _spawnChance;
    }
}