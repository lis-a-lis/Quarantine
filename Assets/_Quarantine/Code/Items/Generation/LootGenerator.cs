using System;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.Items.Implementation;
using UnityEngine;
using Random = UnityEngine.Random;
using Cysharp.Threading.Tasks;

namespace _Quarantine.Code.Items.Generation
{
    public class LootGenerator : MonoBehaviour
    {
        [ItemIDSelector]
        [SerializeField] private string _itemId = "RubbishBagl";
        private IItemDatabaseService _itemDatabaseService;
        private LootSpawnPoint[] _spawnPoints;
        private bool _spawn;

        public void Initialize(IItemDatabaseService itemDatabase)
        {
            _itemDatabaseService = itemDatabase;
            
            FindSpawnPoints();
        }

        private void Start()
        {
            _spawn = true;
            SpawnLootAsync().Forget();
        }

        private void OnDisable()
        {
            _spawn = false;
        }

        private void FindSpawnPoints()
        {
            _spawnPoints = FindObjectsByType<LootSpawnPoint>(FindObjectsSortMode.None);
        }

        private async UniTaskVoid SpawnLootAsync()
        {
            /*foreach (LootSpawnPoint spawnPoint in _spawnPoints)
            {*/
                /*if (Random.value <= spawnPoint.SpawnChance)
                {*/
                //}

                while (_spawn)
                {
                    Item item = _itemDatabaseService.CreateItemInstance("RubbishBagI");
                    item.gameObject.transform.position = transform.position;
                    item.gameObject.SetActive(true);

                    await UniTask.Delay(2000);
                }
                //}
        }
    }
}