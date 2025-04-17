using _Quarantine.Code.InventorySystem.Items.Behavior;
using UnityEngine;

namespace _Quarantine.Code.InventorySystem.Items.Configuration
{
    [CreateAssetMenu(fileName = "Create Item Config", menuName = "Items")]
    public class ItemConfig : ScriptableObject
    {
        [Header("Instances")]
        [SerializeField] private GameObject _prefab;
        [SerializeField] private OldItem _hudInstancePrefab;
            
        [Header("View data")]
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;

        [Header("Technical parameters")]
        [SerializeField] private float _mass; 
        [SerializeField] private float _durability;
        [SerializeField] [Range(1, 100)] private int _stackSize = 1;
        
        public string ID => name;
        
        public string Name => _name;
        
        public string Description => _description;
        
        public Sprite Icon => _icon;
        
        public int StackSize => _stackSize;
        
        public float Durability => _durability;

        public GameObject Prefab => _prefab;

        public OldItem HUDInstancePrefab => _hudInstancePrefab;
    }
}