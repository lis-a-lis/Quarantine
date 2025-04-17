using _Quarantine.Code.Stats.Providers;

namespace _Quarantine.Code.Effects
{
    public interface IEffect
    {
        public float Duration { get; set; }
        
        public void ApplyEffect(IEntityStatsProvider statsProvider, float influenceDuration);
    }
}