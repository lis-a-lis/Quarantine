using _Quarantine.Code.GameEntities;
using _Quarantine.Code.Infrastructure.Services.AssetsManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.Services.EntitiesCreation
{
    public class EntitiesFactory : IEntitiesFactory
    {
        private readonly PlayerFactory _playerFactory;
        private readonly IAssetsProvider _assetsProvider;

        public EntitiesFactory(IAssetsProvider assetsProvider)
        {
            _playerFactory = new PlayerFactory();
            _assetsProvider = assetsProvider;
        }
        private const string PlayerPrefabPath = "Player";
        
        public async UniTask<PlayerEntity> CreatePlayer()
        {
            PlayerEntity prefab = _assetsProvider.LoadPrefab<PlayerEntity>(PlayerPrefabPath);
            
            var player = await Object.InstantiateAsync(prefab, 1);

            PlayerEntity playerEntity = player[0];
            
            return playerEntity;
        }

        public PlayerEntity CreatePlayerEntity()
        {
            PlayerEntity prefab = _assetsProvider.LoadPrefab<PlayerEntity>(PlayerPrefabPath);

            var playerEntity = Object.Instantiate(prefab);
            
            Debug.Log("1. PLAYER CREATED");
            
            return playerEntity;
        }
    }
}