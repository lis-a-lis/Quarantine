using System;
using System.Collections.Generic;
using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration;
using _Quarantine.Code.Items.DatabaseTable;
using _Quarantine.Code.Items.Implementation;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Quarantine.Code.Infrastructure.Services.ItemDatabase
{
    public class ItemDatabaseService : IItemDatabaseService, IItemConfigurationProvider
    {
        private const string PathToItemsTable = "Items/Table";
        
        private readonly Dictionary<string, ItemDatabaseTableCell> _database;
        private readonly Dictionary<Type, string> _itemsIDByType;
        private readonly ISetupItemVisitor _setupItemVisitor;
        
        public ItemDatabaseService()
        {
            _setupItemVisitor = new SetupItemVisitor(this);
            
            _database = new Dictionary<string, ItemDatabaseTableCell>();
            _itemsIDByType = new Dictionary<Type, string>();
            
            List<ItemDatabaseTableCell> table = Resources.Load<ItemDatabaseTable>(PathToItemsTable).Cells;

            foreach (var cell in table)
            {
                _database.Add(cell.ID, cell);
                _itemsIDByType.Add(cell.GetType(), cell.ID);
            }
        }
        
        public string GetItemName(string itemID)
        {
            if (!_database.TryGetValue(itemID, out ItemDatabaseTableCell cell))
                throw new KeyNotFoundException($"Item with ID {itemID} not found");
            
            return cell.Name;
        }

        public string GetItemDescription(string itemID)
        {
            if (!_database.TryGetValue(itemID, out var cell))
                throw new KeyNotFoundException($"Item with ID {itemID} not found");
            
            return cell.Description;
        }

        public Sprite GetItemIcon(string itemID)
        {
            if (!_database.TryGetValue(itemID, out var cell))
                throw new KeyNotFoundException($"Item with ID {itemID} not found");

            return cell.Icon;
        }
        
        public TItemConfiguration GetItemConfiguration<TItemConfiguration>(string itemID) where TItemConfiguration : ItemConfiguration
        {
            if (!_database.TryGetValue(itemID, out var cell))
                throw new KeyNotFoundException($"Item with ID {itemID} not found");
            
            return cell.Configuration as TItemConfiguration;
        }

        public TItemConfiguration GetItemConfiguration<TItemConfiguration>()
            where TItemConfiguration : ItemConfiguration
        {
            return GetItemConfiguration<TItemConfiguration>(_itemsIDByType[typeof(TItemConfiguration)]);
        }
        
        public Item CreateItemInstance(string itemID)
        {
            if (!_database.TryGetValue(itemID, out var cell))
                throw new KeyNotFoundException($"Item with ID {itemID} not found");

            Item instance = Object.Instantiate(cell.Prefab);
            
            instance.Setup(GetItemConfiguration<ItemConfiguration>(itemID));
            
            if (instance is not IVisitableItem)
                throw new KeyNotFoundException($"Item with ID {itemID} not found");
            
            ((IVisitableItem)instance).Accept(_setupItemVisitor);
            
            return instance;
        }
        
        public TItemInstance CreateItemInstanceAs<TItemInstance>(string itemID) where TItemInstance : Item
        {
            return CreateItemInstance(itemID) as TItemInstance;
        }
    }
}