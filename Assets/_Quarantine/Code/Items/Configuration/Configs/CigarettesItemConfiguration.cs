using UnityEngine;

namespace _Quarantine.Code.Items.Configuration.Configs
{
    [CreateAssetMenu(menuName = "Create CigaretteItemConfiguration", fileName = "Items/CigaretteItemConfiguration", order = 0)]
    public class CigarettesItemConfiguration : ItemConfiguration
    {
        [SerializeField] private float _mindHealthBonusByUse = 10;
        
        public float MindHealthBonusByUse => _mindHealthBonusByUse;
    }
}