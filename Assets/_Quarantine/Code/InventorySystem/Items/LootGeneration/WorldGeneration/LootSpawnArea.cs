using System.Collections.Generic;
using _Quarantine.Code.InventorySystem.Items.Database;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Serialization;

namespace _Quarantine.Code.InventorySystem.Items.LootGeneration.WorldGeneration
{   
    public class LootSpawnArea : MonoBehaviour
    {
        [SerializeField] private Color _gizmoColor = Color.green;
        [SerializeField] [Range(0, 1)] private float _gizmosSphereAlpha = 0.4f;
        [FormerlySerializedAs("_spawnDelay")]
        [Space]
        [SerializeField] private float _spawnRadius = 5;
        [SerializeField] private float _spawnDelayInSeconds = 60;
        [SerializeField] private List<LootData> _loot;
        
        private IItemDatabase _itemDatabase;
        private bool _isSpawnActive;
        private int _spawnableItemIndex;

        public void Initialize(IItemDatabase itemDatabase)
        {
            _itemDatabase = itemDatabase;
        }

        public void Activate()
        {
            _isSpawnActive = true;
            SpawnAsync().Forget();
        }

        public void Deactivate()
        {
            _isSpawnActive = false;
        }

        private async UniTaskVoid SpawnAsync()
        {
            await UniTask.WaitForSeconds(_spawnDelayInSeconds);
            
            while (_isSpawnActive)
            {
                LootData lootData = _loot[_spawnableItemIndex];
                
                SpawnItem(_itemDatabase, lootData.ItemID, lootData.SpawnChance);

                _spawnableItemIndex = Mathf.Clamp(_spawnableItemIndex + 1, 0, _loot.Count - 1);
                
                await UniTask.WaitForSeconds(_spawnDelayInSeconds);
            }
        }
        
        private void SpawnItem(IItemDatabase itemDatabase, string itemID, float spawnChance)
        {
            if (Random.value > spawnChance)
                return;
            
            GameObject item = itemDatabase.InstantiatePickableItem(itemID);
            item.transform.position = transform.position;
            item.transform.SetParent(transform);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireSphere(transform.position, _spawnRadius);

            Gizmos.color = 
                new Color(_gizmoColor.r, _gizmoColor.g, _gizmoColor.b, _gizmosSphereAlpha);
            Gizmos.DrawSphere(transform.position, _spawnRadius);
        }
    }
}