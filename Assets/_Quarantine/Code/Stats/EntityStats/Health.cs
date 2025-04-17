using System;

namespace _Quarantine.Code.Stats.EntityStats
{
    public class Health
    {
        private readonly float _max;
        private float _value;
        
        public event Action OnDied;
        public event Action<float> OnChanged;
        
        public Health(float max, float value)
        {
            if (value > max || max <= 0 || value < 0)
                throw new ArgumentException("Value is greater than max value");
            
            _max = max;
            _value = value;
        }

        public float Max => _max;

        public float Value
        {
            get => _value;
            private set
            {
                _value = value;
                OnChanged?.Invoke(value);
                
                if (value <= 0)
                    OnDied?.Invoke();
            }
        }

        public void ApplyDamage(float damage)
        {
            if (damage <= 0)
                throw new ArgumentException("damage is zero or negative");
            
            Value -= damage;
        }

        public void Restore(float amount)
        {
            if (amount <= 0)
                throw new ArgumentException("amount is zero or negative");
            
            Value += amount;
        }
    }
}