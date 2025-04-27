using System;
using System.Collections.Generic;

namespace _Quarantine.Code.GameProgression.Days.Loot
{
    [Serializable]
    public class DailyRatioLootList
    {
        public List<DailyLootData> lootList = new List<DailyLootData>();
    }
}