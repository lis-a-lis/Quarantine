using System;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration.Configs;

namespace _Quarantine.Code.Items.Implementation
{
    public class CigarettesPack : Item, ISetupItem<CigarettesPackItemConfiguration>
    {
        public void Setup(CigarettesPackItemConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public void Accept(ISetupItemVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}