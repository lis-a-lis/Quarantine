using _Quarantine.Code.GameEntities;
using _Quarantine.Code.Infrastructure.PersistentProgress;
using _Quarantine.Code.Items.Implementation;

namespace _Quarantine.Code.Infrastructure.Services.SaveLoad
{
    public interface ISavableEntitiesVisitor
    {
        public GameProgress GameProgress { get; }
        public void SaveData(PlayerEntity playerEntity);
        public void SaveData(Item item);
    }
}