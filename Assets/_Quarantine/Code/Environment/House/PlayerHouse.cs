using System;
using System.Collections.Generic;
using _Quarantine.Code.Infrastructure.PersistentProgress;
using UnityEngine;

namespace _Quarantine.Code.Environment.House
{
    public class PlayerHouse : MonoBehaviour
    {
        
    }

    [Serializable]
    public class PlayerHouseSaveData
    {
        public List<ItemSaveData> items;

        public PlayerHouseSaveData(List<ItemSaveData> itemsData)
        {
            items = itemsData;
        }
    }
}