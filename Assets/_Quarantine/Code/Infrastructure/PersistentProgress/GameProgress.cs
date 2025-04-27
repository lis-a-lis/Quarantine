using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.PersistentProgress
{
    [Serializable]
    public class GameProgress
    {
        public PlayerSaveData player = new PlayerSaveData(
            new PlayerTransformSaveData(Vector3.up, Quaternion.identity, Quaternion.identity),
            new PlayerInventorySaveData(-1, new List<ItemSaveData>()),
            new PlayerStatsSaveData(0, 0, 0, 0, 0));
        
        public List<ItemSaveData> items = new List<ItemSaveData>();
    }
}