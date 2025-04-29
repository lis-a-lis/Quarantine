using UnityEngine;
using _Quarantine.Code.Stats;
using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration.Configs;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;

namespace _Quarantine.Code.Items.Implementation
{
    public class BeefCan : Item, ISetupItem<FoodItemConfiguration>, IUsableStuff
    {
        private float _calories;
        
        public void Setup(FoodItemConfiguration configuration)
        {
            _calories = configuration.SatietyBonus;
        }

        public void Accept(ISetupItemVisitor visitor) =>
            visitor.Visit(this);

        public bool TryUse(PlayerStats stats)
        {
            if (Durability == 0)
                return false;

            Debug.Log(stats);
            stats.AddEffect(new TemporaryEffect(StatsType.Satiety, _calories, 1));
            Durability -= 1;
            
            return true;
        }
    }
}