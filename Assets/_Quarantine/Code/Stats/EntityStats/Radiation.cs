using System;

namespace _Quarantine.Code.Stats.EntityStats
{
    public class Radiation
    {
        private float _value;
        
        public event Action<float> OnChanged;
        
        public Radiation(float value)
        {
            if (value < 0)
                throw new ArgumentException("Value is invalidate");
            
            _value = value;
        }
        
        public float Value
        {
            get => _value;
            private set
            {
                _value = value;
                OnChanged?.Invoke(value);
            }
        }

        public void Decrease(float amount)
        {
            if (amount <= 0)
                throw new ArgumentException("damage is zero or negative");
            
            Value -= amount;
        }

        public void Increase(float amount)
        {
            if (amount <= 0)
                throw new ArgumentException("amount is zero or negative");
            
            Value += amount;
        }
    }
}