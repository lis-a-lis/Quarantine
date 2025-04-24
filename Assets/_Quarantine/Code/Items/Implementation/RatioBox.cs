using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration.Configs;

namespace _Quarantine.Code.Items.Implementation
{
    public class RatioBox : Item, ISetupItem<BoxItemConfiguration>
    {
        /*public void AddItem(Item item)
        {
            
        }*/


        public void Setup(BoxItemConfiguration configuration)
        {
            
        }

        public void Accept(ISetupItemVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}