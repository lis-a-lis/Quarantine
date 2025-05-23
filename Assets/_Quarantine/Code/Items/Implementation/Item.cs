using _Quarantine.Code.Infrastructure.PersistentProgress;
using UnityEngine;
using _Quarantine.Code.Items.Configuration;

namespace _Quarantine.Code.Items.Implementation
{
    public class Item : MonoBehaviour, ISavable<ItemSaveData>, ILoadable<ItemSaveData>
    {
        private string _id;
        private float _durability;

        public string Id => _id;
        public float Durability
        {
            get => _durability;
            protected set => _durability = value;
        }

        public void Setup(ItemConfiguration configuration)
        {
            _id = configuration.ID;
            _durability = configuration.Durability;
        }

        public ItemSaveData Save()
        {
            return new ItemSaveData(_id, _durability, true, transform.position, transform.rotation);
        }

        public void Load(ItemSaveData data)
        {
            
        }
    }
}