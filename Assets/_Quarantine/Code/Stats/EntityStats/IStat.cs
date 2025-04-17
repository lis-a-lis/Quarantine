using System;

namespace _Quarantine.Code.Stats.EntityStats
{
    public interface IStat
    {
        public float Max { get; }
        public float Value { get; }
        public event Action<float> OnChanged;
    }
}