using _Quarantine.Code.GameEntities;
using _Quarantine.Code.Items.Implementation;
using _Quarantine.Code.Infrastructure.PersistentProgress;

namespace _Quarantine.Code.Infrastructure.Services.SaveLoad
{
    public class SavableEntitiesVisitor : ISavableEntitiesVisitor
    {
        private GameProgress _progress;
        
        public GameProgress GameProgress => _progress;

        public SavableEntitiesVisitor(GameProgress gameProgress)
        {
            _progress = gameProgress;
        }
        
        public void SaveData(PlayerEntity playerEntity)
        {
            _progress.player = playerEntity.Save();
        }

        public void SaveData(Item item)
        {
            _progress.items.Add(item.Save());
        }
    }
}