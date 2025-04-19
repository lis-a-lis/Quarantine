using System;
using _Quarantine.Code.GameEntities;
using _Quarantine.Code.Infrastructure.PersistentProgress;
using Cysharp.Threading.Tasks;

namespace _Quarantine.Code.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadEntity
    {
        public void AcceptSave(ISavableEntitiesVisitor visitor);
        //public void AcceptLoad(ILoadableEntitiesVisitor visitor);
    }
    
    public interface ISavableEntitiesVisitor
    {
        public GameProgress GameProgress { get; }
        public void SaveData(PlayerEntity playerEntity);
    }
    
    public class SavableEntitiesVisitor : ISavableEntitiesVisitor
    {
        private GameProgress _gameProgress = new GameProgress();
        
        public GameProgress GameProgress => _gameProgress;

        public void SaveData(PlayerEntity playerEntity)
        {
            _gameProgress.player = playerEntity.Save();
        }
    }

    /*public interface ILoadableEntitiesVisitor
    {
        public void LoadPlayerData(Player player);
    }*/
    
    public interface IProgressSaveLoadService
    {
        public GameProgress LastLoadedProgress { get; }
        public UniTaskVoid Save(GameProgress progress, Action onComplete = null);
        public UniTaskVoid Load(Action<GameProgress> onComplete);
    }
}