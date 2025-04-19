using _Quarantine.Code.GameEntities;
using _Quarantine.Code.Infrastructure.Services.AssetsManagement;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.Services.EntitiesCreation
{
    public class PlayerFactory
    {
        private const string PlayerPrefabPath = "Player";
        
        public PlayerEntity CreatePlayer(IAssetsProvider assetsProvider)
        {
            PlayerEntity player = Object.Instantiate(assetsProvider.LoadPrefab<PlayerEntity>(PlayerPrefabPath));

            return player;
        }
    }
}