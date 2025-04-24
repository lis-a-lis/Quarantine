using System.Collections.Generic;
using UnityEngine;
using _Quarantine.Code.InventoryManagement;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;

namespace _Quarantine.Code.UI.HUD.PlayerInventory
{
    public class InventoryHUDPresenter : MonoBehaviour
    {
        // TODO: filed to Factory
        [SerializeField] private SlotView _slotViewPrefab;
        [SerializeField] private Transform _inventorySlotViewsContainer;

        private readonly List<SlotView> _slotViews = new List<SlotView>();
        private IObservablePlayerInventory _inventory;
        private IItemDatabaseService _itemDatabaseService;

        public void Initialize(IObservablePlayerInventory inventory, IItemDatabaseService itemDatabaseService)
        {
            _itemDatabaseService = itemDatabaseService;
            _inventory = inventory;

            _inventory.ItemAdded += OnItemAdded;
            _inventory.ItemRemoved += OnItemRemoved;
            _inventory.SlotSelected += OnSlotSelected;
            _inventory.SlotUnselected += OnSlotUnselected;

            CreateSlotViews();
        }

        private void CreateSlotViews()
        {
            for (int i = 0; i < _inventory.SlotsAmount; i++)
            {
                _slotViews.Add(Instantiate(_slotViewPrefab, _inventorySlotViewsContainer));
                _slotViews[i].ClearView();
            }
        }
        private void OnDestroy()
        {
            _inventory.ItemAdded -= OnItemAdded;
            _inventory.ItemRemoved -= OnItemRemoved;
            _inventory.SlotSelected -= OnSlotSelected;
            _inventory.SlotUnselected -= OnSlotUnselected;
        }

        private void OnSlotSelected(int slotIndex)
        {
            _slotViews[slotIndex].SetSlotSelected();
        }

        private void OnSlotUnselected(int slotIndex)
        {
            if (_inventory.IsSlotSelected)
                _slotViews[slotIndex].SetSlotUnselected();
        }

        private void OnItemRemoved(int slotIndex)
        {
            _slotViews[slotIndex].ClearView();
        }

        private void OnItemAdded(int slotIndex)
        {
            Debug.Log(slotIndex);
            _inventory.TryGetItemIdBySlotIndex(slotIndex, out string id);
            _slotViews[slotIndex].SetItemView(_itemDatabaseService.GetItemIcon(id));
        }
    }
}