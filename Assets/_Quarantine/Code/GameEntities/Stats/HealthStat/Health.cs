using System;
using UnityEngine;

namespace _Quarantine.Code.GameEntities.Stats.HealthStat
{
    public class Health : IHealth
    {
        private readonly float _max;
        private float _value;

        public float Max => _max;

        public float Value
        { 
            get => _value;
            private set
            {
                if (value > _max)
                    throw new ArgumentOutOfRangeException();
                
                if (value < 0)
                    throw new ArgumentOutOfRangeException();
                
                _value = Mathf.Clamp(value, 0, _max);
                
                OnValueChanged?.Invoke(_value);
                
                if (_value == 0)
                    OnDeath?.Invoke();
            }
        }
        
        public event Action OnDeath;
        public event Action<float> OnValueChanged;
        
        public void ApplyDamage(float damage)
        {
            _value -= damage;
        }

        public void RestoreHealth(float health)
        {
            _value += health;
        }
    }
}