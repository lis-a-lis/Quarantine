using _Quarantine.Code.InventorySystem.Items.Configuration;
using UnityEngine;

namespace _Quarantine.Code.InventorySystem.Items.Behavior
{
    public abstract class OldItem : MonoBehaviour
    {
        public void PickUp()
        {
            gameObject.SetActive(false);
        }

        public void Drop()
        {
            gameObject.SetActive(true);
        }

        public virtual void Initialize(ItemConfig config)
        {
            
        }
    }
}