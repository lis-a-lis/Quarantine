using System;

namespace _Quarantine.Code.GameProgression.Days.Loot
{
    [Serializable]
    public class DailyLootData
    {
        [ItemIDSelector] public string itemID;
    }
}