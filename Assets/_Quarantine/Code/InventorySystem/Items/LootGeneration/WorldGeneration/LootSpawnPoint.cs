using _Quarantine.Code.InventorySystem.Items.Database;
using UnityEngine;

namespace _Quarantine.Code.InventorySystem.Items.LootGeneration.WorldGeneration
{
    public class LootSpawnPoint : MonoBehaviour
    {
        [SerializeField] private LootData _lootData;
        
        private bool _isSpawned;

        public void Spawn(IItemDatabase itemDatabase)
        {
            if (_isSpawned)
                return;
            
            if (Random.value > _lootData.SpawnChance)
                return;
            
            GameObject item = itemDatabase.InstantiatePickableItem(_lootData.ItemID);
            item.transform.position = transform.position;
            item.transform.SetParent(transform);
            
            _isSpawned = true;
        }
    }
}