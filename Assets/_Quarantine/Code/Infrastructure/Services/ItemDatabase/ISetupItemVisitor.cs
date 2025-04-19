using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration.Configs;

namespace _Quarantine.Code.Infrastructure.Services.ItemDatabase
{
    public interface ISetupItemVisitor
    {
        public void Visit(ISetupItem<FoodItemConfiguration> food);
        public void Visit(ISetupItem<VaccineItemConfiguration> vaccine);
        public void Visit(ISetupItem<CigarettesItemConfiguration> cigarettes);
        public void Visit(ISetupItem<RubbishBagItemConfiguration> rubbishBag);
    }
}