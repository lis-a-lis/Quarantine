using UnityEngine;

namespace _Quarantine.Code.Items.Configuration.Configs
{
    [CreateAssetMenu(menuName = "Create Knife", fileName = "Knife", order = 0)]
    public class KnifeItemConfiguration : ItemConfiguration
    {
        [SerializeField] private float _damage;
        
        public float Damage => _damage; 
    }
}