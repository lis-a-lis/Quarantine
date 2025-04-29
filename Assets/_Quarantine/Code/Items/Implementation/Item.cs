using UnityEngine;
using _Quarantine.Code.Items.Configuration;
using _Quarantine.Code.Infrastructure.Services.SaveLoad;
using _Quarantine.Code.Infrastructure.PersistentProgress;

namespace _Quarantine.Code.Items.Implementation
{
    public class Item : MonoBehaviour, ISavable<ItemSaveData>, ILoadable<ItemSaveData>, ISavableEntity
    {
        private string _id;
        private int _durability;

        public string Id => _id;
        public int Durability
        {
            get => _durability;
            protected set => _durability = value;
        }

        public void Setup(ItemConfiguration configuration)
        {
            _id = configuration.ID;
            _durability = configuration.Durability;
        }

        public ItemSaveData Save() =>
            new ItemSaveData(_id, _durability, transform.position, transform.rotation);

        public void Load(ItemSaveData data)
        {
            transform.position = data.position;
            transform.rotation = data.rotation;
            _durability = data.durability;
        }

        public void AcceptSave(ISavableEntitiesVisitor visitor)
        {
            visitor.SaveData(this);
        }
    }
}