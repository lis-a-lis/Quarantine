using UnityEngine;
using _Quarantine.Code.GameEntities;

namespace _Quarantine.Code.Infrastructure.Services.EntitiesCreation
{
    public interface IEntitiesFactory
    {
        public PlayerEntity CreatePlayerEntity(Vector3 position, Quaternion rotation);
    }
}