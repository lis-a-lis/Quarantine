using System.Collections.Generic;
using UnityEngine;
using _Quarantine.Code.InventoryManagement;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;

namespace _Quarantine.Code.UI.HUD.InventoryHUD
{
    public class PlayerInventoryHUDPresenter : MonoBehaviour
    {
        [SerializeField] private InventorySlotView _inventorySlotViewPrefab;
        [SerializeField] private Transform _inventorySlotContainer;
        
        private readonly List<InventorySlotView> _slotViews = new List<InventorySlotView>();
        private IItemDatabaseService _itemDatabaseService;
        private IObservableInventory _inventory;
        private int _selectedSlotIndex = -1;
        private bool _selectedSlotFilling = false;

        public void Initialize(IObservableInventory inventory, IItemDatabaseService itemDatabaseService)
        {
            _inventory = inventory;
            _itemDatabaseService = itemDatabaseService;
                
            _inventory.ItemAdded += OnItemAdded;
            _inventory.ItemRemoved += OnItemRemoved;
            _inventory.SelectedSlotChanged += OnSelectedSlotChanged;
            
            CreateSlotViews();
            UpdateAllViews();
        }

        private void CreateSlotViews()
        {
            for (int i = 0; i < _inventory.SlotsAmount; i++)
            {
                _slotViews.Add(Instantiate(_inventorySlotViewPrefab, _inventorySlotContainer));
                _slotViews[i].ClearView();
            }
        }

        private void UpdateAllViews()
        {
            for (int i = 0; i < _slotViews.Count; i++)
            {
                if (_inventory.TryGetItemData(i, out var id, out var durability))
                {
                    Debug.Log($"{i} slot updated");
                    UpdateSlotView(i);
                }
            }
            
            if (_inventory.IsSlotSelected)
                OnSelectedSlotChanged(_inventory.SelectedSlotIndex);
        }
        
        private void OnDestroy()
        {
            _inventory.ItemAdded -= OnItemAdded;
            _inventory.ItemRemoved -= OnItemRemoved;
            _inventory.SelectedSlotChanged -= OnSelectedSlotChanged;
        }
        
        private void OnSelectedSlotChanged(int slotIndex)
        {
            if (_selectedSlotIndex != -1)
                _slotViews[_selectedSlotIndex].SetSlotSelectionStatus(false, _selectedSlotFilling);
            
            if (slotIndex != -1)
            {
                _slotViews[slotIndex].SetSlotSelectionStatus(true, _inventory.IsSelectedSlotFilled);
                _selectedSlotIndex = slotIndex;
                _selectedSlotFilling = _inventory.IsSelectedSlotFilled;
            }
        }
        
        private void OnItemAdded(int itemIndex)
        {
            UpdateSlotView(itemIndex);
        }

        private void OnItemRemoved(int slotIndex)
        {
            _selectedSlotFilling = false;
            _slotViews[slotIndex].ClearView();
        }

        private void UpdateSlotView(int slotIndex)
        {
            _slotViews[slotIndex].SetItemIcon(GetItemIcon(slotIndex));
            _slotViews[slotIndex].SetItemDurability(1);
            _slotViews[slotIndex].SetSlotSelectionStatus(false, true);
        }

        private Sprite GetItemIcon(int slotIndex)
        {
            _inventory.TryGetItemData(slotIndex, out string itemId, out float itemDurability);
            return _itemDatabaseService.GetItemIcon(itemId);
        }
    }
}