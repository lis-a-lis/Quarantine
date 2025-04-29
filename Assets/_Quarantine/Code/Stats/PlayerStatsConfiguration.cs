using UnityEngine;

namespace _Quarantine.Code.Stats
{
    [CreateAssetMenu(menuName = "Player/Create PlayerStatsConfiguration", fileName = "PlayerStatsConfiguration", order = 0)]
    public class PlayerStatsConfiguration : ScriptableObject
    {
        [SerializeField] private float _maxHealth = 100;
        [SerializeField] private float _maxMind = 100;
        [SerializeField] private float _maxSatiety = 100;
        [SerializeField] private float _maxWater = 100;
        
        [SerializeField] private float _defaultSatietyDecrease = 1;
        [SerializeField] private float _defaultWaterDecrease = 1;
        
        public float MaxHealth => _maxHealth;
        public float MaxMind => _maxMind;
        public float MaxSatiety => _maxSatiety;
        public float MaxWater => _maxWater;
        
        public float DefaultSatietyDecrease => _defaultSatietyDecrease;
        public float DefaultWaterDecrease => _defaultWaterDecrease;
    }
}