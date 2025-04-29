using UnityEngine;

namespace _Quarantine.Code.Items.Configuration.Configs
{
    [CreateAssetMenu(menuName = "Items/Create CigarettesPack ItemConfiguration", fileName = "CigarettesPack ItemConfiguration", order = 0)]
    public class CigarettesPackItemConfiguration : ItemConfiguration
    {
        [SerializeField] private int _cigarettesCount = 20;
    }
}