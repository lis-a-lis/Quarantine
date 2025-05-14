using UnityEngine;
using _Quarantine.Code.Stats;
using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration.Configs;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;

namespace _Quarantine.Code.Items.Implementation
{
    public class Peaches : Item, ISetupItem<FoodItemConfiguration>, IUsableStuff
    {
        [SerializeField] private MeshRenderer _closedCanRenderer;
        [SerializeField] private MeshRenderer _filledCanRenderer;
        [SerializeField] private MeshRenderer _emptyCanRenderer;
        
        private bool _isClosed = true;
        private bool _isFilled = true;
        
        private float _water;
        private float _calories;
        private float _maxDurability;
        private float _durabilityPercentByUse;
        
        public void Setup(FoodItemConfiguration configuration)
        {
            _closedCanRenderer.enabled = true;
            _filledCanRenderer.enabled = false;
            _emptyCanRenderer.enabled = false;
            
            _maxDurability = configuration.Durability;
            _calories = configuration.SatietyBonus;
            _water = configuration.WaterBonus;
        }

        public void Accept(ISetupItemVisitor visitor) =>
            visitor.Visit(this);

        public bool TryUse(PlayerStats stats)
        {
            if (Durability == 0)
                return false;

            if (_isClosed)
            {
                Open();
                return true;
            }

            Debug.Log(stats);
            stats.AddEffect(new TemporaryEffect(StatsType.Satiety, _calories * 1 / _maxDurability, 1));
            stats.AddEffect(new TemporaryEffect(StatsType.Water, _water * 1 / _maxDurability, 1));
            
            Durability -= 1;
            ChangeFilling();
            
            return true;
        }

        private void Open()
        {
            _closedCanRenderer.enabled = false;
            _filledCanRenderer.enabled = true;
            _isClosed = false;
        }

        private void ChangeFilling()
        {
            if (Durability == 0)
            {
                _filledCanRenderer.enabled = false;
                _emptyCanRenderer.enabled = true;
            }
        }
    }
}