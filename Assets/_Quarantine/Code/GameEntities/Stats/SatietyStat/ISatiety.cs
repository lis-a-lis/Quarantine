using System;

namespace _Quarantine.Code.GameEntities.Stats.SatietyStat
{
    public interface ISatiety : ILimitedStat
    {
        public event Action<HungerLevel> OnHungerAppeared;
        public void Increase(float calories);
        public void Decrease(float calories);
    }
}