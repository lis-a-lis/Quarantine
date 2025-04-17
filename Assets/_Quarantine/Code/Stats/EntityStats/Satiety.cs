using System;

namespace _Quarantine.Code.Stats.EntityStats
{
    public class Satiety
    {
        private readonly float _max;
        private float _value;
        
        public event Action OnHungerStarted;
        public event Action<float> OnChanged;
        
        public Satiety(float max, float value)
        {
            if (value > max || max <= 0 || value < 0)
                throw new ArgumentException("Value is invalidate");
            
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
                    OnHungerStarted?.Invoke();
            }
        }

        public void Decrease(float calories)
        {
            if (calories <= 0)
                throw new ArgumentException("damage is zero or negative");
            
            Value -= calories;
        }

        public void Increase(float calories)
        {
            if (calories <= 0)
                throw new ArgumentException("amount is zero or negative");
            
            Value += calories;
        }
    }
}