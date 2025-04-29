using UnityEngine;

namespace _Quarantine.Code.Items.Configuration
{
    [CreateAssetMenu(fileName = "Base Item Configuration", menuName = "Inventory System/Base Item Configuration")]
    public class ItemConfiguration : ScriptableObject
    {
        [SerializeField] private int _durability;

        public string ID => name;
        public int Durability => _durability;
    }
}