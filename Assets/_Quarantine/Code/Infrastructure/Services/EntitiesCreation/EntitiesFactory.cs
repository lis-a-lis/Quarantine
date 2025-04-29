using UnityEngine;
using _Quarantine.Code.GameEntities;
using _Quarantine.Code.Infrastructure.Services.AssetsManagement;

namespace _Quarantine.Code.Infrastructure.Services.EntitiesCreation
{
    public class EntitiesFactory : IEntitiesFactory
    {
        private const string PlayerPrefabPath = "Player";
        
        private readonly IAssetsProvider _assetsProvider;

        public EntitiesFactory(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public PlayerEntity CreatePlayerEntity(Vector3 position, Quaternion rotation)
        {
            PlayerEntity prefab = _assetsProvider.LoadPrefab<PlayerEntity>(PlayerPrefabPath);

            PlayerEntity playerEntity = Object.Instantiate(prefab, position, rotation);
            
            Debug.Log("PLAYER CREATED");
            
            return playerEntity;
        }
    }
}