using System;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration.Configs;

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