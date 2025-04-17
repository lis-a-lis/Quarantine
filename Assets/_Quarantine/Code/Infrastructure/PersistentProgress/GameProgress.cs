using System;

namespace _Quarantine.Code.Infrastructure.PersistentProgress
{
    public interface ISavable<TData>
    {
        public TData Save();
    }

    public interface ILoadable<TData>
    {
        public void Load(TData data);
    }
    
    [Serializable]
    public class GameProgress
    {
        public PlayerSaveData player;
    }
}