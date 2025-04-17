using _Quarantine.Code.GameEntities;
using _Quarantine.Code.Infrastructure.Services.AssetsManagement;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.Services.EntitiesCreation
{
    public class PlayerFactory
    {
        private const string PlayerPrefabPath = "Player";
        
        public Player CreatePlayer(IAssetsProvider assetsProvider)
        {
            return Object.Instantiate(assetsProvider.LoadPrefab<Player>(PlayerPrefabPath));
        }
    }
}