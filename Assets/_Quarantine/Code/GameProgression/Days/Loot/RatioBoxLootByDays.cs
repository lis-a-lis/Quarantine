using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Quarantine.Code.GameProgression.Days.Loot
{
    [CreateAssetMenu(menuName = "Game Progression/RatioBox Loot By Days", fileName = "RatioBox Loot By Days", order = 0)]
    public class RatioBoxLootByDays : ScriptableObject
    {
        [SerializeField] private List<DailyRatioLootList> _ratioBoxLoot = new List<DailyRatioLootList>();

        public string[] GetDailyLootIdList(int day)
        {
            if (day <= 0 || day >= _ratioBoxLoot.Count)
                throw new ArgumentOutOfRangeException();
            
            string[] ids = new string[_ratioBoxLoot[day].lootList.Count];

            for (int i = 0; i < _ratioBoxLoot[day].lootList.Count; i++)
                ids[i] = _ratioBoxLoot[day].lootList[i].itemID;
            
            return ids;
        }
    }

    public class RatioBoxGenerator : MonoBehaviour
    {
        
    }
}