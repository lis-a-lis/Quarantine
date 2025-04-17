using System;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration.Configs;

namespace _Quarantine.Code.Items.Implementation
{
    public class CigarettesPack : Item, ISetupItem<CigarettesItemConfiguration>
    {
        public void Setup(CigarettesItemConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public void Accept(ISetupItemVisitor visitor)
        {
            throw new NotImplementedException();
        }
    }
}