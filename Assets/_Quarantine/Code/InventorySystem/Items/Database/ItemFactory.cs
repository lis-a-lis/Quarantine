using _Quarantine.Code.InventorySystem.Items.Behavior;
using _Quarantine.Code.InventorySystem.Items.Configuration;
using UnityEngine;

namespace _Quarantine.Code.InventorySystem.Items.Database
{
    public class ItemFactory
    {
        public OldItem CreateItem(IItemDatabase itemDatabase, string itemID)
        {
            OldItem prefab = itemDatabase.GetConfig(itemID).HUDInstancePrefab;
            
            OldItem oldItem = Object.Instantiate(prefab);
            
            InitializeItem(itemDatabase, itemID, oldItem);
            
            return oldItem;
        }

        private void InitializeItem(IItemDatabase itemDatabase, string itemID, OldItem oldItem)
        {
            oldItem.Initialize(itemDatabase.GetConfig(itemID));
            
            switch (oldItem)
            {   
                case IGunItem gun:
                    gun.Initialize(itemDatabase.GetConfigAs<GunConfig>(itemID));
                    break;
                case IFoodItem food:
                    food.Initialize(itemDatabase.GetConfigAs<FoodConfig>(itemID));
                    break;
                case IDrinkItem drink:
                    drink.Initialize(itemDatabase.GetConfigAs<DrinkConfig>(itemID));
                    break;
                case IAmmoItem ammo:
                    ammo.Initialize(itemDatabase.GetConfigAs<AmmoConfig>(itemID));
                    break;
            }
        }
    }
}