using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration;
using _Quarantine.Code.Items.Configuration.Configs;

namespace _Quarantine.Code.Infrastructure.Services.ItemDatabase
{
    public class SetupItemVisitor : ISetupItemVisitor
    {
        private readonly IItemConfigurationProvider _configurationProvider;
        
        public SetupItemVisitor(IItemConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public void Visit(ISetupItem<FoodItemConfiguration> food)
        {
            Setup(food);
        }

        public void Visit(ISetupItem<VaccineItemConfiguration> vaccine)
        {
            Setup(vaccine);
        }

        public void Visit(ISetupItem<CigarettesItemConfiguration> cigarettes)
        {
            Setup(cigarettes);
        }

        private void Setup<TItemConfiguration>(ISetupItem<TItemConfiguration> item)
            where TItemConfiguration : ItemConfiguration
        {
            item.Setup(_configurationProvider.GetItemConfiguration<TItemConfiguration>());
        }
    }
}