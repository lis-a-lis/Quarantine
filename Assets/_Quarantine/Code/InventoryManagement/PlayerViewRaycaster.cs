using UnityEngine;
using _Quarantine.Code.GameEntities;

namespace _Quarantine.Code.InventoryManagement
{
    [RequireComponent(typeof(PlayerEntity))]
    public class PlayerViewRaycaster : MonoBehaviour
    {
        [SerializeField] private Transform _viewTransform;
        [SerializeField] private float _maxRayDistance;
        [SerializeField] private LayerMask _ignoreLayer;

        public Vector3 ViewDirection => _viewTransform.forward;
        
        public bool Raycast(out GameObject hitObject)
        {
            bool result = Raycast(out RaycastHit hit);
            hitObject = hit.collider.gameObject;
            return result;
        }

        public bool Raycast(out RaycastHit hit)
        {
            if (Physics.Raycast(_viewTransform.position, _viewTransform.forward,
                    out hit, _maxRayDistance, ~_ignoreLayer))
                return true;

            return false;
        }
        
        public bool Raycast(out RaycastHit hit, LayerMask layerMask)
        {
            if (Physics.Raycast(_viewTransform.position, _viewTransform.forward,
                    out hit, _maxRayDistance, layerMask))
                return true;

            return false;
        }

        public bool Raycast<TExpectedComponent>(out TExpectedComponent expectedComponent)
            where TExpectedComponent : class
        {
            expectedComponent = null;
            
            if (!Raycast(out RaycastHit hit))
                return false;

            return hit.collider.gameObject.TryGetComponent(out expectedComponent);
        }
        
        public bool RaycastTrigger<TExpectedComponent>(out TExpectedComponent expectedComponent, LayerMask layer)
            where TExpectedComponent : class
        {
            expectedComponent = null;
            
            if (!Physics.Raycast(_viewTransform.position, _viewTransform.forward,
                    out RaycastHit hit, _maxRayDistance, ~_ignoreLayer & layer, QueryTriggerInteraction.Collide))
                return false;
            
            /*if (!Raycast(out RaycastHit hit, layer))
                return false;*/

            return hit.collider.gameObject.TryGetComponent(out expectedComponent);
        }
    }
}