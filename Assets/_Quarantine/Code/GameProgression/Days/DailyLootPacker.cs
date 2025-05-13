using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using _Quarantine.Code.Items.Implementation;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;

namespace _Quarantine.Code.GameProgression.Days
{
    public class DailyLootPacker
    {
        private const int MaxAttemptsPerItem = 5;

        private readonly IItemDatabaseService _itemDatabaseService;

        private Transform _boxTransform;
        private Vector3 _boxSize;
        private float _placementStep = 0.1f;
        private List<MeshFilter> _itemMeshes;
        private List<Bounds> _occupiedSpaces;
        private Rigidbody _boxRigidbody;

        public DailyLootPacker(IItemDatabaseService itemDatabaseService)
        {
            _itemDatabaseService = itemDatabaseService;
        }

        public void PackItems(string[] itemIds, string boxId, Vector3 boxPosition)
        {
            _occupiedSpaces = new List<Bounds>();

            CreateBoxInstance(boxId, boxPosition);

            CreateItemInstances(itemIds);

            foreach (var meshFilter in _itemMeshes)
            {
                bool placed = false;
                int attempts = 0;

                while (!placed && attempts < MaxAttemptsPerItem)
                {
                    Vector3 currentSize = GetRotatedSize(meshFilter.sharedMesh.bounds.size, attempts);
                    Quaternion rotation = GetRotation(attempts);
                    Debug.Log($"{meshFilter}-{attempts}: {currentSize} rotation: {rotation}");

                    if (TryFindPosition(currentSize, out Vector3 position))
                    {
                        meshFilter.transform.localPosition = _boxTransform.position
                                                             + position
                                                             + currentSize / 2
                                                             - new Vector3(_boxSize.x, 0, _boxSize.z) / 2;

                        meshFilter.transform.localRotation = rotation;

                        if (meshFilter.transform.localRotation == Quaternion.Euler(0, 0, 90))
                            meshFilter.transform.position += new Vector3(currentSize.x / 2, 0, 0);

                        meshFilter.GetComponent<Rigidbody>().isKinematic = false;
                        placed = true;
                    }

                    attempts++;
                }

                if (!placed)
                    Debug.LogWarning($"Не удалось разместить предмет размером {meshFilter.sharedMesh.bounds.size}");
            }
            
            _boxRigidbody.isKinematic = false;
        }

        private void CreateBoxInstance(string boxId, Vector3 boxPosition)
        {
            Item box = _itemDatabaseService.CreateItemInstance(boxId);

            box.transform.position = boxPosition;

            _boxTransform = box.transform;
            _boxSize = box.GetComponent<BoxCollider>().bounds.size;
            _boxRigidbody = box.GetComponent<Rigidbody>();
            _boxRigidbody.isKinematic = true;
        }

        private void CreateItemInstances(string[] itemIds)
        {
            _itemMeshes = new List<MeshFilter>();

            foreach (var id in itemIds)
            {
                Item item = _itemDatabaseService.CreateItemInstance(id);
                item.GetComponent<Rigidbody>().isKinematic = true;
                _itemMeshes.Add(item.GetComponent<MeshFilter>());
                CalculatePlaceStep(_itemMeshes[^1].sharedMesh.bounds.size);
            }

            _itemMeshes = _itemMeshes.OrderByDescending(mesh => mesh.sharedMesh.bounds.size.x *
                                                                mesh.sharedMesh.bounds.size.y *
                                                                mesh.sharedMesh.bounds.size.z).ToList();
        }

        private void CalculatePlaceStep(Vector3 size)
        {
            float minSizeBound = Mathf.Min(size.x, size.y, size.z) / 4;
            _placementStep = Mathf.Min(_placementStep, minSizeBound);
        }

        private Vector3 GetRotatedSize(Vector3 originalSize, int attempt)
        {
            return attempt switch
            {
                0 => originalSize,
                1 => new Vector3(originalSize.x, originalSize.z, originalSize.y),
                2 => new Vector3(originalSize.z, originalSize.y, originalSize.x),
                3 => new Vector3(originalSize.y, originalSize.x, originalSize.z),
                _ => originalSize
            };
        }

        private Quaternion GetRotation(int attempt)
        {
            return attempt switch
            {
                0 => Quaternion.identity,
                1 => Quaternion.Euler(90f, 0f, 0f),
                2 => Quaternion.Euler(0f, 90f, 0f),
                3 => Quaternion.Euler(0f, 0f, 90f),
                _ => Quaternion.identity
            };
        }

        private bool TryFindPosition(Vector3 itemSize, out Vector3 position)
        {
            for (float x = 0; x <= _boxSize.x - itemSize.x; x += _placementStep)
            {
                for (float y = 0; y <= _boxSize.y - itemSize.y; y += _placementStep)
                {
                    for (float z = 0; z <= _boxSize.z - itemSize.z; z += _placementStep)
                    {
                        position = new Vector3(x, y, z);
                        Bounds bounds = new Bounds(
                            _boxTransform.position + position + itemSize / 2,
                            itemSize
                        );

                        if (IsPositionValid(bounds))
                        {
                            _occupiedSpaces.Add(bounds);
                            return true;
                        }
                    }
                }
            }

            position = Vector3.zero;

            return false;
        }

        private bool IsPositionValid(Bounds newBounds)
        {
            Bounds boxBounds = new Bounds(
                _boxTransform.position + _boxSize / 2,
                _boxSize
            );

            if (!boxBounds.Contains(newBounds.min) || !boxBounds.Contains(newBounds.max))
                return false;

            foreach (var bounds in _occupiedSpaces)
            {
                if (bounds.Intersects(newBounds))
                    return false;
            }

            return true;
        }
    }
}