using _Quarantine.Code.InventorySystem.Items.Configuration;
using UnityEngine;

namespace _Quarantine.Code.InventorySystem.Items.Behavior.Implementation
{
    public class DoubleBarreledShotgun : OldItem, IGunItem
    {
        [SerializeField] private ParticleSystem _fireParticles;
        [SerializeField] private ParticleSystem _smokeParticles;
        
        private Animator _animator;

        public void Reload()
        {
            _smokeParticles.Play();
        }

        public void Fire()
        {
            _fireParticles.Play();
        }

        public void Initialize(GunConfig config)
        {
            throw new System.NotImplementedException();
        }
    }
}