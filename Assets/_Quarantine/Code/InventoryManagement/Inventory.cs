using System;
using UnityEngine;
using System.Collections.Generic;
using _Quarantine.Code.Infrastructure.PersistentProgress;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.InventorySystem.Items.Database;
using _Quarantine.Code.Items.Implementation;
using UnityEditor.PackageManager;

namespace _Quarantine.Code.InventoryManagement
{
    public class Inventory : MonoBehaviour,
        ISavable<PlayerInventorySaveData>, ILoadable<PlayerInventorySaveData> 
    {
        [SerializeField] private int _slotsAmount = 5;

        private IItemDatabaseService _itemsDatabase;
        private List<Item> _slots;
        private int _selectedSlotIndex = -1;
        
        public int SelectedSlotIndex => _selectedSlotIndex;
        public Item SelectedItem => _slots[_selectedSlotIndex];
        public bool IsItemSelected => _selectedSlotIndex != -1;

        private void Awake()
        {
            _slots = new List<Item>();
            
            for (int i = 0; i < _slots.Count; i++)
                _slots.Add(null);
        }

        public void Setup(IItemDatabaseService itemsDatabase)
        {
            _itemsDatabase = itemsDatabase;
        }

        public void SelectSlot(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= _slots.Count)
                throw new IndexOutOfRangeException();

            if (_slots[itemIndex] == null)
                _selectedSlotIndex = itemIndex;

            if (_selectedSlotIndex == itemIndex)
                _selectedSlotIndex = -1;
            
            _selectedSlotIndex = itemIndex;
        }

        public bool AddItem(Item item)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                if (_slots[i] == null)
                {
                    _slots[i] = item;
                    return true;
                }
            }
            return false;
        }

        public bool DropSelectedItem(out Item item)
        {
            item = null;
            
            if (!IsItemSelected)
                return false;
            
            item = SelectedItem;
            _slots[_selectedSlotIndex] = null;
            _selectedSlotIndex = -1;
            
            return true;
        }

        public PlayerInventorySaveData Save()
        {
            List<ItemSaveData> slotsData = new List<ItemSaveData>();

            foreach (Item item in _slots)
                slotsData.Add(item.Save());
            
            PlayerInventorySaveData data = new PlayerInventorySaveData(_selectedSlotIndex, slotsData);

            return data;
        }

        public void Load(PlayerInventorySaveData data)
        {
            _selectedSlotIndex = data.selectedSlotIndex;
            
            foreach (var slotData in data.slots)
            {
                Item item = _itemsDatabase.CreateItemInstance(slotData.id);
                
                item.Load(slotData);
                
                _slots.Add(item);
            }
        }
    }
}