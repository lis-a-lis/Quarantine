using System;

namespace _Quarantine.Code.Stats
{
    [Serializable]
    public class TemporaryEffect
    {
        public StatsType type;
        public float value;
        public float duration;
        
        public TemporaryEffect(StatsType type, float value, float duration)
        {
            this.type = type;
            this.value = value;
            this.duration = duration;
        }
    }
}