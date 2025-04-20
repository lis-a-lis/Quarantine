using System;
using UnityEngine;
using System.Collections.Generic;
using _Quarantine.Code.Items.Implementation;
using _Quarantine.Code.Infrastructure.PersistentProgress;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;

namespace _Quarantine.Code.InventoryManagement
{
    public class Inventory : MonoBehaviour, IObservableInventory,
        ISavable<PlayerInventorySaveData>, ILoadable<PlayerInventorySaveData>
    {
        [SerializeField] private int _slotsAmount = 5;

        private IItemDatabaseService _itemsDatabase;
        private List<Item> _slots;
        private int _selectedSlotIndex = -1;

        public event Action<int> ItemAdded;
        public event Action<int> ItemRemoved;
        public event Action<int> SelectedSlotChanged;
        
        public int SlotsAmount => _slotsAmount;
        public bool IsSlotSelected => _selectedSlotIndex != -1;
        public bool IsSelectedSlotFilled => SelectedItem != null;
        public int SelectedSlotIndex => _selectedSlotIndex;
        public Item SelectedItem => _slots[_selectedSlotIndex];
        public bool SlotSelectionStatus(int slotIndex) => slotIndex == _selectedSlotIndex;

        private void Awake()
        {
            _slots = new List<Item>();
            
            for (int i = 0; i < _slotsAmount; i++)
                _slots.Add(null);
        }

        public void Setup(IItemDatabaseService itemsDatabase)
        {
            _itemsDatabase = itemsDatabase;
        }
        
        public bool TryGetItemData(int slotIndex, out string itemId, out float itemDurability)
        {
            itemId = null;
            itemDurability = 0;
            
            if (_slots[slotIndex] == null)
                return false;

            itemId = _slots[slotIndex].Id;
            itemDurability = _slots[slotIndex].Durability;
            
            return true;
        }

        public bool TryGetItem(int slotIndex, out Item item)
        {
            item = null;
            
            if (_slots[slotIndex] == null)
                return false;
            
            item = _slots[slotIndex];
            
            return true;
        }

        public void SelectSlot(int itemIndex)
        {
            if (itemIndex < 0 || itemIndex >= _slots.Count)
                throw new IndexOutOfRangeException();

            if (_selectedSlotIndex == itemIndex)
            {
                _selectedSlotIndex = -1;
                SelectedSlotChanged?.Invoke(_selectedSlotIndex);
                return;
            }
            
            _selectedSlotIndex = itemIndex;
            SelectedSlotChanged?.Invoke(_selectedSlotIndex);
        }

        public bool AddItem(Item item)
        {
            for (int i = 0; i < _slots.Count; i++)
            {
                if (_slots[i] == null)
                {
                    _slots[i] = item;
                    Debug.Log($"Adding item {item.Id} to inventory slot {i}");
                    ItemAdded?.Invoke(i);
                    return true;
                }
            }
            return false;
        }

        public bool DropSelectedItem(out Item item)
        {
            item = null;
            
            if (!IsSlotSelected || SelectedItem == null)
                return false;
            
            item = SelectedItem;
            _slots[_selectedSlotIndex] = null;
            ItemRemoved?.Invoke(_selectedSlotIndex);
            _selectedSlotIndex = -1;
            
            return true;
        }

        public PlayerInventorySaveData Save()
        {
            List<ItemSaveData> slotsData = new List<ItemSaveData>();

            foreach (Item item in _slots)
            {
                if (item != null)
                    slotsData.Add(item.Save());
            }
            
            PlayerInventorySaveData data = new PlayerInventorySaveData(_selectedSlotIndex, slotsData);

            return data;
        }

        public void Load(PlayerInventorySaveData data)
        {
            _selectedSlotIndex = data.selectedSlotIndex;

            for (int i = 0; i < data.slots.Count; i++)
            {
                Item item = _itemsDatabase.CreateItemInstance(data.slots[i].id);
                item.gameObject.SetActive(false);
                item.transform.SetParent(transform, false);
                item.transform.localPosition = Vector3.one;
                
                item.Load(data.slots[i]);
                
                _slots[i] = item;
            }
        }
    }
}