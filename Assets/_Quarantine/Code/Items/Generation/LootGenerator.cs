using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.Items.Implementation;
using UnityEngine;
using Random = UnityEngine.Random;
using Cysharp.Threading.Tasks;

namespace _Quarantine.Code.Items.Generation
{
    public class LootGenerator : MonoBehaviour
    {
        private IItemDatabaseService _itemDatabaseService;
        private LootSpawnPoint[] _spawnPoints;

        public void Initialize(IItemDatabaseService itemDatabase)
        {
            _itemDatabaseService = itemDatabase;
            
            FindSpawnPoints();
        }

        private void Start()
        {
            SpawnLootAsync().Forget();
        }

        private void FindSpawnPoints()
        {
            _spawnPoints = FindObjectsByType<LootSpawnPoint>(FindObjectsSortMode.None);
        }

        private async UniTaskVoid SpawnLootAsync()
        {
            foreach (LootSpawnPoint spawnPoint in _spawnPoints)
            {
                if (Random.value <= spawnPoint.SpawnChance)
                {
                    Item item = _itemDatabaseService.CreateItemInstance(spawnPoint.ItemID);
                    item.gameObject.transform.position = spawnPoint.transform.position;
                    item.gameObject.SetActive(true);
                }

                await UniTask.Yield();
            }
        }
    }
}