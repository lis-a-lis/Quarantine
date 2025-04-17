using _Quarantine.Code.Stats.EntityStats;

namespace _Quarantine.Code.Stats.Providers
{
    public interface IEntityStatsProvider
    {
        public Health Health { get; }
    }
}