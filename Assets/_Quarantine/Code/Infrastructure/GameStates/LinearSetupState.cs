using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using _Quarantine.Code.GameEntities;
using _Quarantine.Code.Items.Generation;
using _Quarantine.Code.InventoryManagement;
using _Quarantine.Code.Items.Implementation;
using _Quarantine.Code.UI.HUD.PlayerInventory;
using _Quarantine.Code.Infrastructure.Services.UI;
using _Quarantine.Code.Infrastructure.Services.SaveLoad;
using _Quarantine.Code.Infrastructure.PersistentProgress;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.Infrastructure.Services.EntitiesCreation;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine.States;

namespace _Quarantine.Code.Infrastructure.GameStates
{
    public class LinearSetupState : IPayloadState<GameProgress>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IItemDatabaseService _itemsDatabase;
        private readonly IEntitiesFactory _entitiesFactory;
        private readonly IHUDFactory _hudFactory;
        private GameProgress _progress;
        private PlayerEntity _player;
        private List<ISaveLoadEntity> _saveLoadEntities;
        private List<Func<UniTask<bool>>> _linearSetupTasks;
        
        public LinearSetupState(IGameStateMachine gameStateMachine, IEntitiesFactory entitiesFactory,
            IItemDatabaseService itemDatabase, IHUDFactory hudFactory)
        {
            _gameStateMachine = gameStateMachine;
            _entitiesFactory = entitiesFactory;
            _itemsDatabase = itemDatabase;
            _hudFactory = hudFactory;
            _saveLoadEntities = new List<ISaveLoadEntity>();

            InitializeSetupTasksList();
        }
        
        public void Enter(GameProgress progress)
        {
            Debug.Log("Setup State Enter");

            _progress = progress;
            _player = null;
            _saveLoadEntities.Clear();
            
            Setup().Forget();
        }

        public void Exit()
        {
            
        }

        private void InitializeSetupTasksList()
        {
            _linearSetupTasks = new List<Func<UniTask<bool>>>()
            {
                SetupPlayer,
                SetupPlayerUseLoadedData,
                CreateItemsGenerator,
                SetupItems,
                CreateInventoryHUD,
            };
        }
        
        private async UniTaskVoid Setup()
        {
            await RunSetupTasks();
            
            _gameStateMachine.Enter<GameplayState, List<ISaveLoadEntity>>(_saveLoadEntities);
        }

        private async UniTask RunSetupTasks()
        {
            bool result;
            
            foreach (var task in _linearSetupTasks)
            {
                result = await task();

                while (!result)
                    await UniTask.Yield();
            }
        }

        private async UniTask<bool> CreateInventoryHUD()
        {
            InventoryHUDPresenter inventoryHUD = _hudFactory.CreateInventoryHUD();
            
            await UniTask.Yield();
            
            inventoryHUD.Initialize(_player.GetComponent<IObservablePlayerInventory>(), _itemsDatabase);

            return true;
        }

        private async UniTask<bool> CreateItemsGenerator()
        {
            var generator = new GameObject("GENERATOR").AddComponent<LootGenerator>();
            generator.Initialize(_itemsDatabase);
            
            await UniTask.Yield();
            
            var boxGenerator = new GameObject("BOXES GENERATOR").AddComponent<BoxesGenerator>();
            boxGenerator.Initialize(_itemsDatabase);

            await UniTask.Yield();
            
            generator.transform.position += Vector3.up * 4;
            boxGenerator.transform.position += Vector3.up * 4 + Vector3.right * 2;
            
            return true;
        }
        
        private async UniTask<bool> SetupItems()
        {
            foreach (var itemData in _progress.items)
            {
                Item item = _itemsDatabase.CreateItemInstance(itemData.id);
                item.Load(itemData);
                
                await UniTask.Yield();
            }
            
            return true;
        }
        
        private async UniTask<bool> SetupPlayer()
        {
            _player = await _entitiesFactory.CreatePlayer();
            _saveLoadEntities.Add(_player);
            
            return true;
        }
        
        private async UniTask<bool> SetupPlayerUseLoadedData()
        {
            _player.Load(_progress.player);
            
            await UniTask.Yield();

            PlayerInventory inventory = _player.GetComponent<PlayerInventory>();
            
            await UniTask.Yield();
            
            inventory.Setup(_itemsDatabase);
            inventory.Load(_progress.player.inventory);
            
            await UniTask.Yield();
            
            _player.GetComponent<PlayerInventoryInteractionsHandler>().Initialize();
            
            return true;
        }
    }
}