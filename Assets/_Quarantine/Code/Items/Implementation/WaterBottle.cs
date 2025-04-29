using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration.Configs;
using _Quarantine.Code.Stats;
using UnityEngine;

namespace _Quarantine.Code.Items.Implementation
{
    public class WaterBottle : Item, ISetupItem<FoodItemConfiguration>, IUsableStuff
    {
        private float _water;
        
        public void Setup(FoodItemConfiguration configuration)
        {
            _water = configuration.WaterBonus;
        }

        public void Accept(ISetupItemVisitor visitor) =>
            visitor.Visit(this);

        public bool TryUse(PlayerStats stats)
        {
            if (Durability == 0)
                return false;
            
            Debug.Log(stats);
            stats.AddEffect(new TemporaryEffect(StatsType.Water, _water, 1));
            Durability -= 1;
            
            return true;
        }
    }
}