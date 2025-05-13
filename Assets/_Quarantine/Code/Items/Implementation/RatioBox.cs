using System.Collections.Generic;
using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration.Configs;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;

namespace _Quarantine.Code.Items.Implementation
{
    public class RatioBox : Item, ISetupItem<BoxItemConfiguration>, IBox
    {
        private bool _isClosed = true;
        private List<Item> _items;
        
        public void Setup(BoxItemConfiguration configuration)
        {
            _items = new List<Item>();
        }

        public void Accept(ISetupItemVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void Open()
        {
            if (!_isClosed)
                return;

            foreach (var item in _items)
                item.gameObject.SetActive(true);
            
            _isClosed = false;
        }
        
        public void AddItemInside(Item item)
        {
            _items.Add(item);
            item.gameObject.SetActive(false);
        }
    }
}