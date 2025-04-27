using _Quarantine.Code.GameEntities;
using _Quarantine.Code.Infrastructure.Services.AssetsManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.Services.EntitiesCreation
{
    public class EntitiesFactory : IEntitiesFactory
    {
        private const string PlayerPrefabPath = "Player";
        
        private readonly PlayerFactory _playerFactory;
        private readonly IAssetsProvider _assetsProvider;

        public EntitiesFactory(IAssetsProvider assetsProvider)
        {
            _playerFactory = new PlayerFactory();
            _assetsProvider = assetsProvider;
        }
        
        public async UniTask<PlayerEntity> CreatePlayerAsync()
        {
            PlayerEntity player = await _playerFactory.CreatePlayerAsync(_assetsProvider);
            //var player = await Object.InstantiateAsync(prefab, 1);

            //PlayerEntity playerEntity = player[0];
            
            return player;
        }

        public PlayerEntity CreatePlayerEntity(Vector3 position, Quaternion rotation, Quaternion cameraRotation)
        {
            PlayerEntity prefab = _assetsProvider.LoadPrefab<PlayerEntity>(PlayerPrefabPath);

            PlayerEntity playerEntity = Object.Instantiate(prefab, position, rotation);
            
            Debug.Log("1. PLAYER CREATED");
            
            return playerEntity;
        }
    }
}