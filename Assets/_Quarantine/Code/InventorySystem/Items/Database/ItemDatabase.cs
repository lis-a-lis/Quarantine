using System;
using System.Collections.Generic;
using _Quarantine.Code.InventorySystem.Items.Behavior;
using _Quarantine.Code.InventorySystem.Items.Configuration;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Quarantine.Code.InventorySystem.Items.Database
{
    public class ItemDatabase : IItemDatabase
    {
        private readonly Dictionary<string, ItemConfig> _configs;
        
        private readonly ItemFactory _itemFactory;

        public ItemDatabase()
        {
            _configs = new Dictionary<string, ItemConfig>();
            _itemFactory = new ItemFactory();

            LoadConfigs();
        }

        public ItemConfig GetConfig(string itemID)
        {
            if (!_configs.TryGetValue(itemID, out ItemConfig config))
                throw new KeyNotFoundException();
            
            return config;
        }

        public TItemConfig GetConfigAs<TItemConfig>(string itemID) where TItemConfig : ItemConfig
        {
            if (!_configs.TryGetValue(itemID, out ItemConfig config))
                throw new KeyNotFoundException();

            if (config is not TItemConfig castedConfig)
                throw new ArgumentException();
            
            return castedConfig;
        }
        
        public GameObject InstantiatePickableItem(string itemID)
        {
            if (!_configs.TryGetValue(itemID, out ItemConfig config))
                throw new KeyNotFoundException();

            if (config.HUDInstancePrefab == null)
                throw new NullReferenceException();
            
            return Object.Instantiate(config.Prefab);
        }
        
        public TItem InstantiateHudItem<TItem>(string itemID) where TItem : OldItem
        {
            if (!_configs.TryGetValue(itemID, out ItemConfig config))
                throw new KeyNotFoundException();

            if (config.HUDInstancePrefab == null)
                throw new NullReferenceException();

            if (config.HUDInstancePrefab is not TItem)
                throw new ArgumentException();

            return _itemFactory.CreateItem(this, itemID) as TItem;
        }
        
        private void LoadConfigs()
        {
            foreach (ItemConfig config in Resources.LoadAll<ItemConfig>(ItemsConstants.PathToConfigs))
                _configs.Add(config.name, config);
        }
    }
}