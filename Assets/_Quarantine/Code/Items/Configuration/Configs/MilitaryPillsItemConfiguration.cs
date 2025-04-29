using UnityEngine;

namespace _Quarantine.Code.Items.Configuration.Configs
{
    [CreateAssetMenu(menuName = "Items/Create MilitaryPills ItemConfiguration", fileName = "MilitaryPills ItemConfiguration", order = 0)] 
    public class MilitaryPillsItemConfiguration : ItemConfiguration
    {
        [SerializeField] private float _experimentalDiseaseProgress = 10;

        public float ExperimentalDiseaseProgress => _experimentalDiseaseProgress;
    }
}