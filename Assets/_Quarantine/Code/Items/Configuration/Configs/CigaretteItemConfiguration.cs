using UnityEngine;

namespace _Quarantine.Code.Items.Configuration.Configs
{
    [CreateAssetMenu(menuName = "Items/Create Cigarette ItemConfiguration", fileName = "Cigarette ItemConfiguration",
        order = 0)]
    public class CigaretteItemConfiguration : ItemConfiguration
    {
        [SerializeField] private float _mindBonusByUse = 10;

        public float MindBonusByUse => _mindBonusByUse;
    }
}