using _Quarantine.Code.GameEntities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.Services.EntitiesCreation
{
    public interface IEntitiesFactory
    {
        public UniTask<PlayerEntity> CreatePlayerAsync();
        public PlayerEntity CreatePlayerEntity(Vector3 position, Quaternion rotation, Quaternion cameraRotation);
    }
}