using System.Collections.Generic;
using UnityEngine;
using _Quarantine.Code.Items.Behaviour;
using _Quarantine.Code.Items.Configuration.Configs;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using UnityEngine.Serialization;

namespace _Quarantine.Code.Items.Implementation
{
    public class RatioBox : Item, ISetupItem<BoxItemConfiguration>, IBox
    {
        [SerializeField] private MeshRenderer _closedBoxRenderer;
        [SerializeField] private BoxCollider _closedBoxCollider;
        [SerializeField] private GameObject _openedBox;
        [SerializeField] private BoxCollider _internalBounds;
        
        private bool _isClosed = true;
        private List<GameObject> _items;
        
        public Vector3 InternalSize => _internalBounds.bounds.size;
        
        public void Setup(BoxItemConfiguration configuration)
        {
            _items = new List<GameObject>();
            _closedBoxRenderer.enabled = true;
            _closedBoxCollider.enabled = true;
            _openedBox.gameObject.SetActive(false);
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
            {
                item.transform.SetParent(null);
                item.gameObject.SetActive(true);
            }
            
            _items.Clear();
            _closedBoxRenderer.enabled = false;
            _closedBoxCollider.enabled = false;
            _openedBox.gameObject.SetActive(true);
            _isClosed = false;
        }
        
        public void AddItemInside(GameObject item)
        {
            _items.Add(item);
            item.transform.SetParent(transform);
            item.gameObject.SetActive(false);
        }
    }
}