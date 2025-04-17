using System;
using System.Collections.Generic;
using _Quarantine.Code.Stats.Providers;
using UnityEngine;

namespace _Quarantine.Code.Effects.EffectHandlers
{
    [RequireComponent(typeof(PlayerStatsProvider))]
    public class PlayerEffectsHandler : MonoBehaviour, IEntityEffectsHandler
    {
        private PlayerStatsProvider _statsProvider;
        private Dictionary<Type, IEffect> _effects;
        
        public event Action<IEffect> OnEffectAdded;
        public event Action<IEffect> OnEffectRemoved;
        
        private void Awake()
        {
            _statsProvider = GetComponent<PlayerStatsProvider>();
        }

        private void Update()
        {
            ApplyEffects();
        }

        private void ApplyEffects()
        {
            foreach (var key in _effects.Keys)
            {
                _effects[key].ApplyEffect(_statsProvider, Time.deltaTime);
                _effects[key].Duration -= Time.deltaTime;

                if (_effects[key].Duration <= 0)
                {
                    _effects.Remove(key);
                    OnEffectRemoved?.Invoke(_effects[key]);
                }
            }
        }

        public void AddEffect(IEffect effect)
        {
            Type effectType = effect.GetType();

            if (_effects.TryGetValue(effectType, out var activeEffect))
            {
                activeEffect.Duration += effect.Duration;
                return;
            }
            
            _effects.Add(effectType, effect);
            OnEffectAdded?.Invoke(effect);
        }

        public void RemoveEffect<TEffect>()
        {
            _effects.Remove(typeof(TEffect));
            OnEffectRemoved?.Invoke(_effects[typeof(TEffect)]);
        }

        public void RemoveAllEffects()
        {
            throw new System.NotImplementedException();
        }
    }
}