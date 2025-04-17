using System;
using UnityEngine;

namespace _Quarantine.Code.GameEntities.Stats.SatietyStat
{
    public class Satiety : ISatiety
    {
        private readonly float _max;
        private float _value;
        private float _hungerValue;
        
        public float Max => _max;

        public Satiety(SatietyData satietyData)
        {
            _max = satietyData.maxSatietyValue;
            _value = satietyData.satietyValue;
            _hungerValue = satietyData.hungerValue;
        }
        
        public float Value
        {
            get => _value;
            private set
            {
                _value = value;
                
                OnValueChanged?.Invoke(_value);
            }
        }
        
        public event Action<float> OnValueChanged;

        public event Action<HungerLevel> OnHungerAppeared;

        public void Increase(float calories)
        {
            Value += calories;
        }

        public void Decrease(float calories)
        {
            Value = Mathf.Clamp(Value - calories, 0, _max);
            _hungerValue -= Value - calories;
            
            if (_hungerValue != 0)
                OnHungerAppeared?.Invoke(HungerLevel.SlightHunger);
        }
    }
}