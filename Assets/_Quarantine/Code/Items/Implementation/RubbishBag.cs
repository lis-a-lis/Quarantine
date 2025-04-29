using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration.Configs;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;

namespace _Quarantine.Code.Items.Implementation
{
    public class RubbishBag : Item, ISetupItem<RubbishBagItemConfiguration>
    {
        public void Setup(RubbishBagItemConfiguration configuration)
        {
            
        }

        public void Accept(ISetupItemVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}