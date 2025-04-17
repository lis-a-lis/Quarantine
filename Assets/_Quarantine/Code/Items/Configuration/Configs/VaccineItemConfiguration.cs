using UnityEngine;

namespace _Quarantine.Code.Items.Configuration.Configs
{
    [CreateAssetMenu(menuName = "Create VaccineItemConfiguration", fileName = "Items/VaccineItemConfiguration", order = 0)]
    public class VaccineItemConfiguration : ItemConfiguration
    {
        [SerializeField] private float _diseaseSlowdown = 20;

        public float DiseaseSlowdown => _diseaseSlowdown;
    }
}