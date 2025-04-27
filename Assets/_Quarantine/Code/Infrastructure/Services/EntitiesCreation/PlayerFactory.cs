using _Quarantine.Code.GameEntities;
using _Quarantine.Code.Infrastructure.Services.AssetsManagement;
using Cysharp.Threading.Tasks;
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

        public async UniTask<PlayerEntity> CreatePlayerAsync(IAssetsProvider assetsProvider)
        {
            await UniTask.NextFrame();
            
            PlayerEntity prefab = assetsProvider.LoadPrefab<PlayerEntity>(PlayerPrefabPath);

            AsyncInstantiateOperation<PlayerEntity> operation = Object.InstantiateAsync(prefab);

            while (!operation.isDone)
            {
                await UniTask.NextFrame();
            }

            return operation.Result[0];
        }
    }
}