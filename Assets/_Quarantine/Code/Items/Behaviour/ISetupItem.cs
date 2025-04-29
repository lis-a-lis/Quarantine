using _Quarantine.Code.Items.Configuration;
using _Quarantine.Code.Stats;

namespace _Quarantine.Code.Items.Behaviour
{
    public interface ISetupItem<TItemConfiguration> : IVisitableItem
        where TItemConfiguration : ItemConfiguration
    {
        public void Setup(TItemConfiguration configuration);
    }

    public interface IEquipment
    {
        public void Equip();
        public void Unequip();
    }

    public interface IUsableStuff
    {
        public bool TryUse(PlayerStats stats);
    }

    public interface ICigarette : IUsableStuff
    {
        public bool IsGlowing { get; }
        public void LightItUp(IFireSource fireSource);
        public void PutOut();
    }

    public interface IFireSource
    {
        public void Fire();
    }
}