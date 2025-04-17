namespace _Quarantine.Code.Effects.EffectHandlers
{
    public interface IEntityEffectsHandler
    {
        public void AddEffect(IEffect effect);
        public void RemoveEffect<TEffect>();
        public void RemoveAllEffects();
    }
}