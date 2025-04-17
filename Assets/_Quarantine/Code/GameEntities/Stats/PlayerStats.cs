using _Quarantine.Code.GameEntities.Stats.HealthStat;
using UnityEngine;

namespace _Quarantine.Code.GameEntities.Stats
{
    public class PlayerStats : MonoBehaviour, IDamageable
    {
        private Health _health;
        
        public IHealth Health => _health;
    }
}