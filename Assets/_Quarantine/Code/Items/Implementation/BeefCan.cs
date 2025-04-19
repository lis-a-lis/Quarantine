using System;
using _Quarantine.Code.Effects;
using _Quarantine.Code.Effects.EffectHandlers;
using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration.Configs;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;

namespace _Quarantine.Code.Items.Implementation
{
    public class BeefCan : Item, ISetupItem<FoodItemConfiguration>
    {
        private float _calories;

        public void Use(IEntityEffectsHandler effectsHandler)
        {
            effectsHandler.AddEffect(new SatietyEffect(_calories, 2f));
        }
        
        public void Setup(FoodItemConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public void Accept(ISetupItemVisitor visitor) =>
            visitor.Visit(this);
    }
}