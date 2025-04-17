using System;

namespace _Quarantine.Code.GameEntities.Stats.HealthStat
{
    public interface IHealth : ILimitedStat
    {
        public event Action OnDeath;  
        public void ApplyDamage(float damage);
        public void RestoreHealth(float health);
    }
}