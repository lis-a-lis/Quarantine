using _Quarantine.Code.Stats.Providers;
using _Quarantine.Code.Stats;
using _Quarantine.Code.Stats.EntityStats;

namespace _Quarantine.Code.Effects
{
    public class SatietyEffect : IEffect
    {
        public SatietyEffect(float amount, float duration)
        {
            Duration = duration;
            
        }
        
        public float Duration { get; set; }
        
        public void ApplyEffect(IEntityStatsProvider statsProvider, float influenceDuration)
        {
            throw new System.NotImplementedException();
        }
    }
}