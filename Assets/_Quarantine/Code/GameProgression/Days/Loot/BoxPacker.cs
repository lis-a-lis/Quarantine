using System.Linq;
using UnityEngine;
using System.Collections.Generic;

namespace _Quarantine.Code.GameProgression.Days.Loot
{
    public class BoxPacker : MonoBehaviour
    {
        public Vector3 boxSize = new Vector3(10, 10, 10);
        public Transform boxTransform;
        public List<Vector3> itemSizes = new List<Vector3>();
        public GameObject itemPrefab; 
        public bool drawGizmos = true; 
        private float _placeStep = 0.5f;
        
        private List<Bounds> _placedItems = new List<Bounds>();

        private void Start()
        {
            PackItems();
        }

        public void PackItems()
        {
            _placedItems.Clear();

            List<Vector3> sizes = itemSizes.OrderByDescending(size => size.x * size.y * size.z).ToList();

            foreach (Vector3 size in sizes)
            {
                if (!TryPlaceItem(size, out Vector3 position))
                {
                    Debug.LogWarning($"Не удалось разместить предмет размером {size}");
                    continue;
                }
                
                var item = Instantiate(itemPrefab, boxTransform.position + position + size / 2, Quaternion.identity, boxTransform);
                item.transform.localScale = size;
                Debug.Log($"Размещен в позиции {position}");
            }
        }

        private bool TryPlaceItem(Vector3 itemSize, out Vector3 position)
        {
            for (float x = 0; x <= boxSize.x - itemSize.x; x += _placeStep)
            {
                for (float y = 0; y <= boxSize.y - itemSize.y; y += _placeStep)
                {
                    for (float z = 0; z <= boxSize.z - itemSize.z; z += _placeStep)
                    {
                        position = new Vector3(x, y, z);
                        
                        Bounds bounds = new Bounds(boxTransform.position + position + itemSize / 2, itemSize);

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
            Bounds boxBounds = new Bounds(boxTransform.position + boxSize / 2, boxSize);
            return boxBounds.Contains(itemBounds.min) && boxBounds.Contains(itemBounds.max);
        }

        private void OnDrawGizmos()
        {
            if (!drawGizmos || !boxTransform)
                return;

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(boxTransform.position + boxSize / 2, boxSize);

            Gizmos.color = Color.green;
            foreach (var item in _placedItems)
                Gizmos.DrawWireCube(item.center, item.size);
        }
    }
}