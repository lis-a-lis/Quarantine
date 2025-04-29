using _Quarantine.Code.Items.Configuration;

namespace _Quarantine.Code.Items.Behaviour
{
    public interface ISetupItem<TItemConfiguration> : IVisitableItem
        where TItemConfiguration : ItemConfiguration
    {
        public void Setup(TItemConfiguration configuration);
    }
}