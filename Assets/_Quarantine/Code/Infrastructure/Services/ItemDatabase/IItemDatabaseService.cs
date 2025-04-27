using _Quarantine.Code.Items.Configuration;
using _Quarantine.Code.Items.Implementation;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.Services.ItemDatabase
{
    public interface IItemDatabaseService
    {
        public string GetItemName(string itemID);
        public string GetItemDescription(string itemID);
        public Sprite GetItemIcon(string itemID);
        public TItemConfiguration GetItemConfiguration<TItemConfiguration>(string itemID) where TItemConfiguration : ItemConfiguration;
        public Item CreateItemInstance(string itemID);
        public TItemInstance CreateItemInstanceAs<TItemInstance>(string itemID) where TItemInstance : Item;
        public Item CreateItemInstanceDeactivated(string itemID);
    }
}