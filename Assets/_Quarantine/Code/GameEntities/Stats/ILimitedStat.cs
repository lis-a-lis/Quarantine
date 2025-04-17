using System;

namespace _Quarantine.Code.GameEntities.Stats
{
    public interface ILimitedStat
    {
        public float Max { get; }
        public float Value { get; }
        public event Action<float> OnValueChanged; 
    }
}