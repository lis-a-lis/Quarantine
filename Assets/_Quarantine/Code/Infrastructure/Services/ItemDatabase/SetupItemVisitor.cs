using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration;
using _Quarantine.Code.Items.Configuration.Configs;

namespace _Quarantine.Code.Infrastructure.Services.ItemDatabase
{
    public class SetupItemVisitor : ISetupItemVisitor
    {
        private readonly IItemDatabaseService _itemDatabaseService;
        
        public SetupItemVisitor(IItemDatabaseService itemDatabaseService)
        {
            _itemDatabaseService = itemDatabaseService;
        }

        public void Visit(ISetupItem<KnifeItemConfiguration> knife) =>
            Setup(knife);
        
        public void Visit(ISetupItem<FoodItemConfiguration> food) =>
            Setup(food);

        public void Visit(ISetupItem<MilitaryPillsItemConfiguration> vaccine) =>
            Setup(vaccine);

        public void Visit(ISetupItem<CigarettesPackItemConfiguration> cigarettes) =>
            Setup(cigarettes);

        public void Visit(ISetupItem<RubbishBagItemConfiguration> rubbishBag) =>
            Setup(rubbishBag);
        
        public void Visit(ISetupItem<BoxItemConfiguration> box) =>
            Setup(box);

        private void Setup<TItemConfiguration>(ISetupItem<TItemConfiguration> item)
            where TItemConfiguration : ItemConfiguration =>
            item.Setup(_itemDatabaseService.GetItemConfiguration<TItemConfiguration>(item.Id));
    }
}