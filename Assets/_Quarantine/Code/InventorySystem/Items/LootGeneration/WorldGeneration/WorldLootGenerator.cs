using System.Collections.Generic;
using UnityEngine;

namespace _Quarantine.Code.InventorySystem.Items.LootGeneration.WorldGeneration
{
    public class WorldLootGenerator : MonoBehaviour
    {
        private List<LootSpawnPoint> _spawnPoints;
        private List<LootSpawnArea> _spawnAreas;
        
        
        
        private void FindAllSpawnPoints()
        {
            _spawnPoints = new List<LootSpawnPoint>();

            _spawnPoints.AddRange(FindObjectsByType<LootSpawnPoint>(FindObjectsSortMode.None));
        }

        private void FindAllSpawnAreas()
        {
            _spawnAreas = new List<LootSpawnArea>();
            
            _spawnAreas.AddRange(FindObjectsByType<LootSpawnArea>(FindObjectsSortMode.None));
        }
    }
}