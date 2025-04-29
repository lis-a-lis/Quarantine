using _Quarantine.Code.Infrastructure.PersistentProgress;

namespace _Quarantine.Code.Infrastructure.Services.SaveLoad
{
    public interface IGameProgressSaveService
    {
        public void Initialize(GameProgress gameProgress);
        public void AddSavableEntity(ISavableEntity savableEntity);
        public void ClearSavableEntities();
        public void Save();
    }
}