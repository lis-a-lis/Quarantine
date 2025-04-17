using System;
using System.Collections.Generic;
using _Quarantine.Code.GameEntities;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine;
using _Quarantine.Code.Infrastructure.GameBehaviourStateMachine.States;
using _Quarantine.Code.Infrastructure.PersistentProgress;
using _Quarantine.Code.Infrastructure.Services.EntitiesCreation;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.InventoryManagement;
using _Quarantine.Code.InventorySystem.Items.Database;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.GameStates
{
    public class SetupState : IPayloadState<GameProgress>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IItemDatabaseService _itemsDatabase;
        private readonly IEntitiesFactory _entitiesFactory;
        private readonly List<UniTask> _setupActions;
        private GameProgress _progress;

        public SetupState(IGameStateMachine gameStateMachine, IEntitiesFactory entitiesFactory,
            IItemDatabaseService itemDatabase)
        {
            _gameStateMachine = gameStateMachine;
            _entitiesFactory = entitiesFactory;
            _itemsDatabase = itemDatabase;
            
            _setupActions = new List<UniTask> {
                SetupPlayer(),
                
            };
        }

        public void Enter(GameProgress gameProgress)
        {
            Debug.Log("Setup State Enter");
            
            _progress = gameProgress;
            
            //Setup(gameProgress, () => _gameStateMachine.Enter<GameplayState>()).Forget();
        }

        public void Exit()
        {
            
        }

        private async UniTaskVoid Setup(GameProgress gameProgress, Action onSetupCompleted = null)
        {
            await UniTask.Yield();
  
            await UniTask.WhenAll(_setupActions);
            
            await UniTask.Yield();
            
            onSetupCompleted?.Invoke();
        }
        
        private async UniTask SetupPlayer()
        {
            Player player = _entitiesFactory.CreatePlayer();
            
            await UniTask.Yield();
            
            player.GetComponent<PlayerInventory>().Setup(_itemsDatabase);
            
            await UniTask.Yield();
            
            player.Load(_progress.player);
            
            await UniTask.Yield();
        }
    }
}