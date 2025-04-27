using UnityEngine;
using System.Collections.Generic;
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
    public class SetupState : IPayloadState<GameProgress>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IItemDatabaseService _itemsDatabase;
        private readonly IEntitiesFactory _entitiesFactory;
        private readonly IHUDFactory _hudFactory;
        private readonly IGameProgressSaveService _progressSaveService;
        private GameProgress _progress;
        private PlayerEntity _player;

        public SetupState(IGameStateMachine gameStateMachine, IEntitiesFactory entitiesFactory,
            IItemDatabaseService itemDatabase, IHUDFactory hudFactory, IGameProgressSaveService progressSaveService)
        {
            _gameStateMachine = gameStateMachine;
            _entitiesFactory = entitiesFactory;
            _itemsDatabase = itemDatabase;
            _hudFactory = hudFactory;
            _progressSaveService = progressSaveService;
        }

        public void Enter(GameProgress progress)
        {
            Debug.Log("Setup State Enter");

            _progress = progress;
            _player = null;
            Setup().Forget();
        }

        public void Exit()
        {
        }

        private async UniTaskVoid Setup()
        {
            await SetupPlayer();
            
            await CreateItemsGenerator();

            await CreateInventoryHUD();

            await SetupItems();

            await UniTask.Yield();
            
            _gameStateMachine.Enter<GameplayState>();
        }
        
        private async UniTask CreateInventoryHUD()
        {
            InventoryHUDPresenter inventoryHUD = _hudFactory.CreateInventoryHUD();

            await UniTask.Yield();

            inventoryHUD.Initialize(_player.GetComponent<IObservablePlayerInventory>(), _itemsDatabase);
        }

        private async UniTask CreateItemsGenerator()
        {
            var generator = new GameObject("GENERATOR").AddComponent<LootGenerator>();
            generator.Initialize(_itemsDatabase, (entity) => _progressSaveService.AddSavableEntity(entity));

            await UniTask.Yield();

            var boxGenerator = new GameObject("BOXES GENERATOR").AddComponent<BoxesGenerator>();
            boxGenerator.Initialize(_itemsDatabase, (entity) => _progressSaveService.AddSavableEntity(entity));

            await UniTask.Yield();

            generator.transform.position += Vector3.up * 4;
            boxGenerator.transform.position += Vector3.up * 4 + Vector3.right * 2;
        }

        private async UniTask SetupItems()
        {
            foreach (var itemData in _progress.items)
            {
                Item item = _itemsDatabase.CreateItemInstance(itemData.id);
                item.Load(itemData);

                await UniTask.Yield();
            }
        }

        private async UniTask SetupPlayer()
        {
            _player = _entitiesFactory.CreatePlayerEntity(
                _progress.player.transform.playerPosition,
                _progress.player.transform.playerRotation,
                _progress.player.transform.cameraRotation);
            
            await UniTask.Yield();
            
            PlayerInventory inventory = _player.GetComponent<PlayerInventory>();
            inventory.Setup(_itemsDatabase);
            inventory.Load(_progress.player.inventory);

            await UniTask.Yield();
            
            _player.GetComponent<PlayerInventoryInteractionsHandler>().Initialize();
            _progressSaveService.AddSavableEntity(_player);
        }
    }
}