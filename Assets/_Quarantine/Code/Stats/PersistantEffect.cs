using System;

namespace _Quarantine.Code.Stats
{
    [Serializable]
    public class PersistantEffect
    {
        public StatsType type;
        public float value;
        
        public PersistantEffect(StatsType type, float value)
        {
            this.type = type;
            this.value = value;
        }
    }
}