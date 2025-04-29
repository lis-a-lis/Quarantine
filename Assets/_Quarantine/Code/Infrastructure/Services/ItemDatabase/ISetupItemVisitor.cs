using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration.Configs;

namespace _Quarantine.Code.Infrastructure.Services.ItemDatabase
{
    public interface ISetupItemVisitor
    {
        public void Visit(ISetupItem<FoodItemConfiguration> food);
        public void Visit(ISetupItem<MilitaryPillsItemConfiguration> vaccine);
        public void Visit(ISetupItem<CigarettesPackItemConfiguration> cigarettes);
        public void Visit(ISetupItem<RubbishBagItemConfiguration> rubbishBag);
        public void Visit(ISetupItem<BoxItemConfiguration> box);
    }
}