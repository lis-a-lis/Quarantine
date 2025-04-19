using _Quarantine.Code.GameEntities;
using Cysharp.Threading.Tasks;

namespace _Quarantine.Code.Infrastructure.Services.EntitiesCreation
{
    public interface IEntitiesFactory
    {
        public UniTask<PlayerEntity> CreatePlayer();
        public PlayerEntity CreatePlayerEntity();

    }
}