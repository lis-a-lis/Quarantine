using UnityEngine;

namespace _Quarantine.Code.Items.Configuration
{
    [CreateAssetMenu(fileName = "Base Item Configuration", menuName = "Inventory System/Base Item Configuration")]
    public class ItemConfiguration : ScriptableObject
    {
        [SerializeField] private float _durability;

        public string ID => name;
        public float Durability => _durability;
    }
}