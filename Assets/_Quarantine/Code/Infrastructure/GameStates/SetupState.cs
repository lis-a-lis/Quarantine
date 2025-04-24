using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using _Quarantine.Code.GameEntities;
using _Quarantine.Code.InventoryManagement;
using _Quarantine.Code.Infrastructure.PersistentProgress;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.Infrastructure.Services.EntitiesCreation;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine.States;
using _Quarantine.Code.Infrastructure.Services.SaveLoad;
using _Quarantine.Code.Infrastructure.Services.UI;
using _Quarantine.Code.Items.Generation;
using _Quarantine.Code.Items.Implementation;
using _Quarantine.Code.UI.HUD.PlayerInventory;

namespace _Quarantine.Code.Infrastructure.GameStates
{
    public class SetupState : IPayloadState<GameProgress>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IItemDatabaseService _itemsDatabase;
        private readonly IEntitiesFactory _entitiesFactory;
        private readonly IHUDFactory _hudFactory;
        private GameProgress _progress;
        private PlayerEntity _player;
        private List<Func<UniTask>> _setupTasks;
        private List<ISaveLoadEntity> _saveLoadEntities;
        
        public SetupState(IGameStateMachine gameStateMachine, IEntitiesFactory entitiesFactory,
            IItemDatabaseService itemDatabase, IHUDFactory hudFactory)
        {
            _gameStateMachine = gameStateMachine;
            _entitiesFactory = entitiesFactory;
            _itemsDatabase = itemDatabase;
            _hudFactory = hudFactory;
            _saveLoadEntities = new List<ISaveLoadEntity>();

            InitializeSetupTasksList();
        }

        private void InitializeSetupTasksList()
        {
            _setupTasks = new List<Func<UniTask>>()
            {
                SetupPlayer,
                LoadPlayerData,
                CreateItemsGenerator,
                SetupItems,
                CreateInventoryHUD,
            };
        }

        private async UniTask CreateInventoryHUD()
        {
            await UniTask.WaitForEndOfFrame();
            
            InventoryHUDPresenter inventoryHUD = _hudFactory.CreateInventoryHUD();
            
            inventoryHUD.Initialize(_player.GetComponent<IObservablePlayerInventory>(), _itemsDatabase);
        }

        private async UniTask CreateItemsGenerator()
        {
            await UniTask.WaitForEndOfFrame();

            var generator = new GameObject("GENERATOR").AddComponent<LootGenerator>();
            var boxGenerator = new GameObject("BOXES GENERATOR").AddComponent<BoxesGenerator>();

            generator.transform.position += Vector3.up * 4;
            boxGenerator.transform.position += Vector3.up * 4 + Vector3.right * 2;
            
            generator.Initialize(_itemsDatabase);
            boxGenerator.Initialize(_itemsDatabase);
        }

        private void ClearExistData()
        {
            _player = null;
            _saveLoadEntities.Clear();
        }

        public void Enter(GameProgress progress)
        {
            Debug.Log("Setup State Enter");

            _progress = progress;
            ClearExistData();
            
            Setup().Forget();
        }

        public void Exit()
        {
            
        }

        private async UniTaskVoid Setup()
        {
            await RunSetupTasks(_setupTasks);
            
            _gameStateMachine.Enter<GameplayState, List<ISaveLoadEntity>>(_saveLoadEntities);
        }

        private async UniTask RunSetupTasks(List<Func<UniTask>> tasks)
        {
            foreach (var task in tasks)
                await task();
        }

        private async UniTask SetupItems()
        {
            await UniTask.WaitForEndOfFrame();

            foreach (var itemData in _progress.items)
            {
                Item item = _itemsDatabase.CreateItemInstance(itemData.id);
                item.Load(itemData);
            }
        }
        
        private async UniTask LoadPlayerData()
        {
            await UniTask.WaitForEndOfFrame();
            
            _player.Load(_progress.player);
            PlayerInventory inventory = _player.GetComponent<PlayerInventory>();
            inventory.Setup(_itemsDatabase);
            inventory.Load(_progress.player.inventory);
            _saveLoadEntities.Add(_player);
            _player.GetComponent<PlayerInventoryInteractionsHandler>().Initialize(inventory.SelectedSlotIndex);
        }

        private async UniTask SetupPlayer()
        {
            _player = await _entitiesFactory.CreatePlayer();
        }
    }
}