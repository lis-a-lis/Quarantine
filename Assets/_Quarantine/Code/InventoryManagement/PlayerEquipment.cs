using _Quarantine.Code.Items.Behaviour;
using UnityEngine;

namespace _Quarantine.Code.InventoryManagement
{
    public class PlayerEquipment : MonoBehaviour
    {
        private IEquipment _equipment;
        private ICigarette _cigarette;

        public bool IsCigaretteEquipped => _cigarette != null;
        public bool IsEquipped => _equipment != null;

        public ICigarette Cigarette => _cigarette;
        
        public void Equip(IEquipment equipment)
        {
        }

        public void Equip(ICigarette cigarette)
        {
            _cigarette = cigarette;
        }

        public void Unequip()
        {
        }
    }
}