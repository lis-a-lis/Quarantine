using UnityEngine;

namespace _Quarantine.Code.Interactable
{
    public class PlayerInteractionsHandler : MonoBehaviour, IInteractionsHandler
    {
        [SerializeField] private Transform _playerHead;
        [SerializeField] private LayerMask _interactableObjectsLayer;
        [SerializeField] private float _interactDistance = 2f;
        
        public void Interact()
        {
            if (!Physics.Raycast(_playerHead.position, _playerHead.forward,
                    out RaycastHit hit, _interactDistance, _interactableObjectsLayer)) return;
            
            hit.collider.gameObject.GetComponent<IInteractable>().Interact();
        }
    }
}