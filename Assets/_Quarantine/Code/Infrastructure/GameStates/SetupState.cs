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

namespace _Quarantine.Code.Infrastructure.GameStates
{
    public class SetupState : IPayloadState<GameProgress>
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IItemDatabaseService _itemsDatabase;
        private readonly IEntitiesFactory _entitiesFactory;
        private GameProgress _progress;
        private List<ISaveLoadEntity> _saveLoadEntities;
        private PlayerEntity _player;
        private List<Func<UniTask>> _setupTasks;
        
        public SetupState(IGameStateMachine gameStateMachine, IEntitiesFactory entitiesFactory,
            IItemDatabaseService itemDatabase)
        {
            _gameStateMachine = gameStateMachine;
            _entitiesFactory = entitiesFactory;
            _itemsDatabase = itemDatabase;
            _saveLoadEntities = new List<ISaveLoadEntity>();

            InitializeSetupTasksList();
        }

        private void InitializeSetupTasksList()
        {
            _setupTasks = new List<Func<UniTask>>()
            {
                SetupPlayer,
                
                LoadPlayerData,
            };
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
            {
                await task();
                /*var t = task();
                await UniTask.WaitUntil();
                */

            }
        }
        
        private async UniTask LoadPlayerData()
        {
            await UniTask.WaitForEndOfFrame();
            
            _player.Load(_progress.player);
            _saveLoadEntities.Add(_player);
            _player.GetComponent<Inventory>().Setup(_itemsDatabase);
        }

        private async UniTask SetupPlayer()
        {
            _player = await _entitiesFactory.CreatePlayer();
        }
    }
}