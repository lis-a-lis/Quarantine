using _Quarantine.Code.Items.Implementation;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;

namespace _Quarantine.Code.Items.Generation
{
    public class RatioBoxLootGenerator
    {
        private readonly IItemDatabaseService _itemsDatabase;

        public RatioBoxLootGenerator(IItemDatabaseService itemsDatabase)
        {
            _itemsDatabase = itemsDatabase;
        }

        /*public RatioBox GenerateRatioBoxLoot()
        {
            
        }*/
    }
}