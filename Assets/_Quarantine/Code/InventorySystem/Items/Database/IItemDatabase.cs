using _Quarantine.Code.InventorySystem.Items.Behavior;
using _Quarantine.Code.InventorySystem.Items.Configuration;
using UnityEngine;

namespace _Quarantine.Code.InventorySystem.Items.Database
{
    public interface IItemDatabase
    {
        public ItemConfig GetConfig(string itemID);
        public TItemConfig GetConfigAs<TItemConfig>(string itemID) where TItemConfig : ItemConfig;
        public GameObject InstantiatePickableItem(string itemID);
        public TItem InstantiateHudItem<TItem>(string itemID) where TItem : OldItem;
    }
}