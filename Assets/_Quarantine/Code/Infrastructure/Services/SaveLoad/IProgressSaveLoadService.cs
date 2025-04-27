using System;
using _Quarantine.Code.Infrastructure.PersistentProgress;
using Cysharp.Threading.Tasks;

namespace _Quarantine.Code.Infrastructure.Services.SaveLoad
{
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