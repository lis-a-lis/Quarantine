using _Quarantine.Code.GameEntities;

namespace _Quarantine.Code.Infrastructure.Services.EntitiesCreation
{
    public interface IEntitiesFactory
    {
        public Player CreatePlayer();
        
    }
}