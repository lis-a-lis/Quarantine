using System.Collections.Generic;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.Items.Configuration.Configs;
using UnityEngine;

namespace _Quarantine.Code.GameProgression.Days
{
    public class RatioBoxGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _generationPosition;
        [ItemIDSelector] [SerializeField] private List<string> _itemIDs;
        
        private IItemDatabaseService _database;
        private DailyLootPacker _packer;
        private string _ratioBoxID;

        public void Initialize(IItemDatabaseService database)
        {
            _database = database;
            _packer = new DailyLootPacker(_database);
            _ratioBoxID = database.GetFirstItemIDByConfigurationType<BoxItemConfiguration>();
        }

        public void GenerateRatioBoxInspector()
        {
            GenerateRatioBox();
        }

        private void GenerateRatioBox()
        {
            _packer.PackItems(_itemIDs.ToArray(), _ratioBoxID, _generationPosition.position);
        }
    }
}