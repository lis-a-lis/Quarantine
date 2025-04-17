using _Quarantine.Code.Stats.Providers;
using _Quarantine.Code.Stats;
using _Quarantine.Code.Stats.EntityStats;

namespace _Quarantine.Code.Effects
{
    public class Damage : IEffect
    {
        private float _value;
        
        public Damage(float damage)
        {
            _value = damage; 
        }
        
        public float Duration { get; set; }
        public void ApplyEffect(IEntityStatsProvider statsProvider, float influenceDuration)
        {
            statsProvider.Health.ApplyDamage(_value);
        }
    }
}