using _Quarantine.Code.GameEntities;
using _Quarantine.Code.Infrastructure.Services.AssetsManagement;

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
        
        public Player CreatePlayer()
        {
            return _playerFactory.CreatePlayer(_assetsProvider);
        }
    }
}