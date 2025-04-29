using System;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration.Configs;
using _Quarantine.Code.Stats;

namespace _Quarantine.Code.Items.Implementation
{
    public class Cigarette : Item, ISetupItem<CigarettesPackItemConfiguration>, ICigarette
    {
        private bool _isGlowing;
        
        public bool IsGlowing => _isGlowing;
        
        public void Setup(CigarettesPackItemConfiguration configuration)
        {
            
        }

        public void Accept(ISetupItemVisitor visitor)
        {
            visitor.Visit(this);
        }
        
        public void LightItUp(IFireSource fireSource)
        {
            
        }

        public void PutOut()
        {
            //if (_isGlowing)
                
        }

        public bool TryUse(PlayerStats stats)
        {
            if (!_isGlowing)
                return false;
            
            stats.AddEffect(new TemporaryEffect(StatsType.Mind, 5, 1));
            return true;
        }
    }
}