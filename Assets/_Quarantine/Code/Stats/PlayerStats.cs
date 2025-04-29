using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Quarantine.Code.Stats
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private PlayerStatsConfiguration _config;

        private Dictionary<StatsType, Action<float, float>> _effectsBehaviour;
        private List<PersistantEffect> _persistantEffects;
        private List<TemporaryEffect> _temporaryEffects;
        
        private float _health;
        private float _mind;
        private float _satiety;
        private float _water;

        public event Action<float> HealthChanged; 
        public event Action<float> MindChanged; 
        public event Action<float> SatietyChanged; 
        public event Action<float> WaterChanged; 

        public float Health => _health;
        public float Mind => _mind;
        public float Satiety => _satiety;
        public float Water => _water;
        
        public float MaxHealth => _config.MaxHealth;
        public float MaxMind => _config.MaxMind;
        public float MaxSatiety => _config.MaxSatiety;
        public float MaxWater => _config.MaxWater;
        
        public void AddEffect(PersistantEffect effect)
        {
            _persistantEffects.Add(effect);
        }
        
        public void AddEffect(TemporaryEffect effect)
        {
            _temporaryEffects.Add(effect);
        }

        private void Awake()
        {
            _health = _config.MaxHealth;
            _mind = _config.MaxMind;
            _satiety = _config.MaxSatiety;
            _water = _config.MaxWater;
            
            _temporaryEffects = new List<TemporaryEffect>();
            
            _persistantEffects = new List<PersistantEffect>()
            {
                new PersistantEffect(StatsType.Satiety, -_config.DefaultSatietyDecrease),
                new PersistantEffect(StatsType.Water, -_config.DefaultWaterDecrease),
            };
        }

        private void Start()
        {
            _effectsBehaviour = new Dictionary<StatsType, Action<float, float>>
            {
                [StatsType.Health] = (value, duration) =>
                {
                    _health = Mathf.Clamp(_health + value * duration, 0, _config.MaxHealth);
                    HealthChanged?.Invoke(_health);
                },
                
                [StatsType.Mind] = (value, duration) =>
                {
                    _mind = Mathf.Clamp(_mind + value * duration, 0, _config.MaxMind);
                    MindChanged?.Invoke(_mind);
                },
                
                [StatsType.Satiety] = (value, duration) =>
                {
                    _satiety = Mathf.Clamp(_satiety + value * duration, 0, _config.MaxSatiety);
                    SatietyChanged?.Invoke(_satiety);
                },
                
                [StatsType.Water] = (value, duration) =>
                {
                    _water = Mathf.Clamp(_water + value * duration, 0, _config.MaxWater);
                    WaterChanged?.Invoke(_water);
                },
            };
        }

        private void Update()
        {
            ApplyEffects(Time.deltaTime);
        }

        private void ApplyEffects(float duration)
        {
            foreach (var effect in _persistantEffects)
                _effectsBehaviour[effect.type](effect.value, duration);
            
            if (_temporaryEffects.Count == 0)
                return;
            
            for (int i = _temporaryEffects.Count - 1; i >= 0; i--)
            {
                _effectsBehaviour[_temporaryEffects[i].type](_temporaryEffects[i].value, duration);
                
                _temporaryEffects[i].duration -= duration;
                
                if (_temporaryEffects[i].duration <= 0)
                    _temporaryEffects.RemoveAt(i);
            }
        }
    }
}