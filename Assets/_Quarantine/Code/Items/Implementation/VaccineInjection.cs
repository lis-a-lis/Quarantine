using System;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration.Configs;

namespace _Quarantine.Code.Items.Implementation
{
    public class VaccineInjection : Item, ISetupItem<VaccineItemConfiguration>
    {
        public void Setup(VaccineItemConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public void Accept(ISetupItemVisitor visitor) =>
            visitor.Visit(this);
    }
}