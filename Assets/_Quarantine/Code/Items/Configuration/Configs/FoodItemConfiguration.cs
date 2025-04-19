using UnityEngine;

namespace _Quarantine.Code.Items.Configuration.Configs
{
    [CreateAssetMenu(menuName = "Create FoodItemConfiguration", fileName = "Items/FoodItemConfiguration", order = 0)]
    public class FoodItemConfiguration : ItemConfiguration
    {
        [SerializeField] private float _satietyBonus = 0;
        [SerializeField] private float _waterBonus = 0;
        
        public float SatietyBonus => _satietyBonus;
        public float WaterBonus => _waterBonus;
    }

    public class FirelightItemConfiguration : ItemConfiguration
    {
        
    }

    [CreateAssetMenu(menuName = "Create RatioBox", fileName = "RatioBox", order = 0)]
    public class RatioBox : ItemConfiguration
    {
        
    }
}