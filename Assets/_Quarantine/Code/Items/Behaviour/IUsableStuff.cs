using _Quarantine.Code.Effects.EffectHandlers;
using _Quarantine.Code.Items.Configuration.Configs;

namespace _Quarantine.Code.Items.Behaviour
{
    public interface IUsableStuff : ISetupItem<FoodItemConfiguration>,
        ISetupItem<VaccineItemConfiguration>,
        ISetupItem<CigarettesItemConfiguration>
    {
        public void Use(IEntityEffectsHandler effectsHandler);
    }
}