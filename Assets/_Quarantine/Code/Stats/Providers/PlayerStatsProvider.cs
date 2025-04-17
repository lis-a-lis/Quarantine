using _Quarantine.Code.Stats.EntityStats;
using UnityEngine;

namespace _Quarantine.Code.Stats.Providers
{
    public class PlayerStatsProvider : MonoBehaviour, IEntityStatsProvider
    {
        public Health Health { get; }
        public Satiety Satiety { get; }
        public Radiation Radiation { get; }

        private void Update()
        {
            
        }
    }
}