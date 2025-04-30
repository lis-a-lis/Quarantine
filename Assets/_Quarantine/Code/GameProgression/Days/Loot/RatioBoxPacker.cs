using System.Collections.Generic;
using System.Linq;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.Items.Implementation;
using UnityEngine;

namespace _Quarantine.Code.GameProgression.Days.Loot
{
    public class RatioBoxPacker
    {
        private readonly IItemDatabaseService _itemDatabaseService;
        private Transform _boxTransform;
        private Vector3 _boxSize;
        private List<Item> _items;
        private List<Vector3> _itemSizes;
        private List<Bounds> _placedItems;
        private float _placeStep = Mathf.Infinity;

        public RatioBoxPacker(IItemDatabaseService itemDatabaseService)
        {
            _itemDatabaseService = itemDatabaseService;
        }
        
        public void Generate(string[] itemIds, Vector3 generationPosition)
        {
            Item box = _itemDatabaseService.CreateItemInstance("Box");
            
            box.GetComponent<Rigidbody>().isKinematic = true;
            box.transform.position = generationPosition;
            
            _boxSize = box.GetComponent<Collider>().bounds.size;
            _boxTransform = box.transform;
            
            InstantiateItems(itemIds);
            CalculateData();

            List<Vector3> sizes = _itemSizes.OrderByDescending(size => size.x * size.y * size.z).ToList();

            foreach (Vector3 size in sizes)
            {
                if (!TryPlaceItem(size, out Vector3 position))
                {
                    Debug.LogWarning($"Не удалось разместить предмет размером {size}");
                    continue;
                }

                PlaceItemWithSize(position, size);

                Debug.Log($"Размещен в позиции {position}");
            }
        }
        
        private void InstantiateItems(string[] ids)
        {
            _items = new List<Item>();
            _itemSizes = new List<Vector3>();
            _placedItems = new List<Bounds>();

            foreach (var id in ids)
            {
                Item item = _itemDatabaseService.CreateItemInstance(id);
                item.GetComponent<Rigidbody>().isKinematic = true;
                _items.Add(item);
            }
        }

        private void CalculateData()
        {
            foreach (var item in _items)
            {
                Vector3 size = item.GetComponent<MeshFilter>().sharedMesh.bounds.size;
                _itemSizes.Add(size);
                CalculateStep(size);
            }
        }

        private void CalculateStep(Vector3 size)
        {
            float minSizeBound = Mathf.Min(size.x, size.y, size.z) / 4;
            
            _placeStep = Mathf.Min(_placeStep, minSizeBound);
        }

        private void PlaceItemWithSize(Vector3 position, Vector3 size)
        {
            Vector3 itemPosition = _boxTransform.position
                                   - new Vector3(_boxSize.x / 2, 0, _boxSize.z / 2)
                                   + position
                                   + new Vector3(size.x / 2, 0, size.z / 2);

            for (int i = 0; i < _itemSizes.Count; i++)
            {
                if (_itemSizes[i] != size)
                    continue;
                    
                _items[i].transform.position = itemPosition;
                _itemSizes.RemoveAt(i);
                _items.RemoveAt(i);
                    
                break;
            }
        }

        private bool TryPlaceItem(Vector3 itemSize, out Vector3 position)
        {
            for (float x = 0; x <= _boxSize.x ; x += _placeStep)
            {
                for (float z = 0; z <= _boxSize.z ; z += _placeStep)
                {
                    for (float y = 0; y <= _boxSize.y ; y += _placeStep)
                    {
                        position = new Vector3(x, y, z);
                        
                        Bounds bounds = new Bounds(_boxTransform.position + position + itemSize / 2, itemSize);

                        bool canPlace = true;
                        
                        foreach (Bounds placed in _placedItems)
                        {
                            if (bounds.Intersects(placed))
                            {
                                canPlace = false;
                                break;
                            }
                        }

                        if (canPlace && IsInsideBox(bounds))
                        {
                            _placedItems.Add(bounds);
                            return true;
                        }
                    }
                }
            }

            position = Vector3.zero;
            
            return false;
        }

        private bool IsInsideBox(Bounds itemBounds)
        {
            Bounds boxBounds = new Bounds(_boxTransform.position + _boxSize / 2, _boxSize);
            return boxBounds.Contains(itemBounds.min) && boxBounds.Contains(itemBounds.max);
        }
    }
}