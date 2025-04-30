using System.Collections.Generic;
using _Quarantine.Code.Infrastructure.PersistentProgress;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.Services.SaveLoad
{
    public class GameProgressSaveService : IGameProgressSaveService
    {
        private readonly IProgressSaveLoadService _progressSaveLoadService;
        private readonly List<ISavableEntity> _savableEntities;
        private ISavableEntitiesVisitor _visitor;
        private GameProgress _progress;

        public GameProgressSaveService(IProgressSaveLoadService progressSaveLoadService)
        {
            _progressSaveLoadService = progressSaveLoadService;
            _savableEntities = new List<ISavableEntity>();
        }

        public void Initialize(GameProgress gameProgress)
        {
            _progress = gameProgress;
            _visitor = new SavableEntitiesVisitor(_progress);
        }
        
        public void AddSavableEntity(ISavableEntity savableEntity)
        {
            Debug.Log($"Add SavableEntity {savableEntity.GetType().Name}");
            _savableEntities.Add(savableEntity);
        }

        public void ClearSavableEntities()
        {
            _savableEntities.Clear();
        }

        public void Save()
        {
            foreach (var entity in _savableEntities)
                entity.AcceptSave(_visitor);

            _progressSaveLoadService.Save(_visitor.GameProgress).Forget();
        }
    }
}