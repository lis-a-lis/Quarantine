using System;
using System.Collections.Generic;
using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Implementation;
using _Quarantine.Code.Stats;
using UnityEngine;

namespace _Quarantine.Code.InventoryManagement
{
    [RequireComponent(typeof(PlayerInventoryInteractionsHandler))]
    public class ItemInteractionsHandler : MonoBehaviour
    {
        private PlayerInventoryInteractionsHandler _inventoryInteractionsHandler;
        private PlayerEquipment _equipment;
        private PlayerStats _stats;

        private Item _currentItem;

        private Dictionary<Type, Action> _interactionsByItemBehaviourType;
        private Action _currentItemBehaviour;

        private void Awake()
        {
            _stats = GetComponent<PlayerStats>();
            _equipment = GetComponent<PlayerEquipment>();
            _inventoryInteractionsHandler = GetComponent<PlayerInventoryInteractionsHandler>();

            _interactionsByItemBehaviourType = new Dictionary<Type, Action>()
            {
                [typeof(ICigarette)] = InteractWithCigarette,
                [typeof(IFireSource)] = InteractWithFireSource,
                [typeof(IUsableStuff)] = InteractWithUsableStuff,
            };
        }

        private void InteractWithCigarette()
        {
            if (!_equipment.Cigarette.IsGlowing)
                return;

            _equipment.Cigarette.TryUse(_stats);
        }

        private void InteractWithUsableStuff()
        {
            ((IUsableStuff)_currentItem).TryUse(_stats);
        }

        private void InteractWithFireSource()
        {
            if (_equipment.IsCigaretteEquipped)
                _equipment.Cigarette.LightItUp((IFireSource)_currentItem);
            else
                ((IFireSource)_currentItem).Fire();
        }

        private void OnEnable()
        {
            _inventoryInteractionsHandler.ItemAppearInHands += ShowInteractions;
            _inventoryInteractionsHandler.ItemDisappearInHands += HideInteractions;
        }
        
        private void OnDisable()
        {
            _inventoryInteractionsHandler.ItemAppearInHands -= ShowInteractions;
            _inventoryInteractionsHandler.ItemDisappearInHands -= HideInteractions;
        }

        public void Interact()
        {
            if (_currentItem == null)
                return;
            
            _currentItemBehaviour.Invoke();
        }

        private void HideInteractions()
        {
            _currentItem = null;
            _currentItemBehaviour = null;
        }

        private void ShowInteractions(Item item)
        {
            _currentItem = item;

            foreach (var behaviourType in _interactionsByItemBehaviourType.Keys)
            {
                if (behaviourType.IsAssignableFrom(item.GetType()))
                {
                    _currentItemBehaviour = _interactionsByItemBehaviourType[behaviourType];
                    break;
                }
            }
            
            Debug.Log(_currentItem);
            Debug.Log(_currentItemBehaviour);
        }
    }
}