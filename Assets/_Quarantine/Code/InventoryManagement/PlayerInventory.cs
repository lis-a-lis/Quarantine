using System;
using System.Collections.Generic;
using UnityEngine;
using _Quarantine.Code.Items.Implementation;
using _Quarantine.Code.Infrastructure.PersistentProgress;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using NUnit.Framework;

namespace _Quarantine.Code.InventoryManagement
{
    public class PlayerInventory : MonoBehaviour, IObservablePlayerInventory,
        ISavable<PlayerInventorySaveData>, ILoadable<PlayerInventorySaveData>
    {
        private const int UnselectedSlotIndex = -1;
        
        [SerializeField] private int _slotsAmount = 5;

        private IItemDatabaseService _itemsDatabase;
        private List<InventorySlot> _slots;
        private int _selectedSlotIndex = UnselectedSlotIndex;
        
        public event Action<int> SlotSelected;
        public event Action<int> SlotUnselected;
        public event Action<int> ItemAdded;
        public event Action<int> ItemRemoved;
        
        public int SlotsAmount => _slotsAmount;
        public int SelectedSlotIndex => _selectedSlotIndex;
        public bool IsSlotSelected => _selectedSlotIndex != UnselectedSlotIndex;

        public bool IsSelectedSlotFilled =>
            IsSlotSelected && _slots[_selectedSlotIndex].IsFilled;

        public bool IsSelectedSlotEmpty =>
            IsSlotSelected && _slots[_selectedSlotIndex].IsEmpty;

        public Item SelectedItem =>
            IsSelectedSlotFilled ? _slots[_selectedSlotIndex].Item : null;
        
        private void Awake()
        {
            _slots = new List<InventorySlot>();

            for (int i = 0; i < _slotsAmount; i++)
                _slots.Add(new InventorySlot());
        }

        public void Setup(IItemDatabaseService itemsDatabase)
        {
            _itemsDatabase = itemsDatabase;
        }
        
        public bool TryGetItemIdBySlotIndex(int slotIndex, out string id)
        {
            id = string.Empty;
            
            if (_slots[slotIndex].IsEmpty)
                return false;

            id = _slots[slotIndex].Item.Id;
            return true;
        }

        public bool TryGetItem(int slotIndex, out Item item)
        {
            item = null;

            if (_slots[slotIndex].IsEmpty)
                return false;

            item = _slots[slotIndex].Item;

            return true;
        }

        public void SelectSlot(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= _slotsAmount)
                throw new IndexOutOfRangeException();

            if (_selectedSlotIndex == slotIndex)
            {
                SlotUnselected?.Invoke(_selectedSlotIndex);
                _selectedSlotIndex = UnselectedSlotIndex;
            }
            else
            {
                if (_selectedSlotIndex != UnselectedSlotIndex)
                    SlotUnselected?.Invoke(_selectedSlotIndex);
                
                _selectedSlotIndex = slotIndex;
                SlotSelected?.Invoke(_selectedSlotIndex);
            }
        }

        public bool AddItem(Item item)
        {
            if (IsSlotSelected && IsSelectedSlotEmpty)
            {
                _slots[_selectedSlotIndex].Item = item;
                ItemAdded?.Invoke(_selectedSlotIndex);
                return true;
            }

            for (int slotIndex = 0; slotIndex < _slotsAmount; slotIndex++)
            {
                if (_slots[slotIndex].IsEmpty)
                {
                    _slots[slotIndex].Item = item;
                    ItemAdded?.Invoke(slotIndex);
                    return true;
                }
            }
            
            return false;
        }

        public bool DropSelectedItem(out Item item)
        {
            item = null;
  
            if (IsSelectedSlotEmpty)
                return false;

            item = _slots[_selectedSlotIndex].Item;
            _slots[_selectedSlotIndex].Item = null;
            ItemRemoved?.Invoke(_selectedSlotIndex);

            return true;
        }
        
        public PlayerInventorySaveData Save()
        {
            List<ItemSaveData> slotsData = new List<ItemSaveData>();

            foreach (InventorySlot slot in _slots)
            {
                if (slot.IsFilled)
                    slotsData.Add(slot.Item.Save());
            }

            return new PlayerInventorySaveData(_selectedSlotIndex, slotsData);
        }

        public void Load(PlayerInventorySaveData data)
        {
            _selectedSlotIndex = data.selectedSlotIndex;
            
            if (IsSlotSelected)
                SlotSelected?.Invoke(_selectedSlotIndex);

            for (int i = 0; i < data.slots.Count; i++)
            {
                Item item = _itemsDatabase.CreateItemInstanceDeactivated(data.slots[i].id);
                item.transform.SetParent(transform, false);
                item.transform.localPosition = Vector3.one;
                item.Load(data.slots[i]);

                _slots[i].Item = item;
                
                ItemAdded?.Invoke(i);
            }
        }
    }
}