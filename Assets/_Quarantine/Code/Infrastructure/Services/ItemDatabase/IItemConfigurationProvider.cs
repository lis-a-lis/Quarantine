using _Quarantine.Code.Items.Configuration;

namespace _Quarantine.Code.Infrastructure.Services.ItemDatabase
{
    public interface IItemConfigurationProvider
    {
        public TItemConfiguration GetItemConfiguration<TItemConfiguration>()
            where TItemConfiguration : ItemConfiguration;
    }
}