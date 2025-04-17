using UnityEngine;

namespace _Quarantine.Code.Items.Generation
{
    public class LootSpawnPoint : MonoBehaviour
    {
        [ItemIDSelector]
        [SerializeField] private string _itemID;
        [SerializeField] private float _spawnChance = 1;

        private bool _isLootSpawned;
        
        public string ItemID => _itemID;
        public float SpawnChance => _spawnChance;
        public bool IsLootSpawned => _isLootSpawned;

        public void SetStateToSpawned()
        {
            _isLootSpawned = true;
        }

        public void SetStateToEmpty()
        {
            _isLootSpawned = false;
        }
    }
}