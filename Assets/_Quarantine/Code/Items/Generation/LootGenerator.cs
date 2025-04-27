using System;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.Infrastructure.Services.SaveLoad;
using _Quarantine.Code.Items.Implementation;
using UnityEngine;
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
        private Action<ISavableEntity> _onItemCreated;

        public void Initialize(IItemDatabaseService itemDatabase, Action<ISavableEntity> onItemCreated)
        {
            _itemDatabaseService = itemDatabase;
            _onItemCreated = onItemCreated;
            //FindSpawnPoints();
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
                    _onItemCreated?.Invoke(item);

                    await UniTask.Delay(2000);
                }
                //}
        }
    }
}