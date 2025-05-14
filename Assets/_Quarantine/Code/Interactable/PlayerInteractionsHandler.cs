using UnityEngine;
using _Quarantine.Code.InventoryManagement;

namespace _Quarantine.Code.Interactable
{
    [RequireComponent(typeof(PlayerViewRaycaster))]
    public class PlayerInteractionsHandler : MonoBehaviour, IInteractionsHandler
    {
        [SerializeField] private LayerMask _interactableLayer;
        
        private PlayerViewRaycaster _playerViewRaycaster;

        private void Awake()
        {
            _playerViewRaycaster = GetComponent<PlayerViewRaycaster>();
        }

        public void Interact()
        {
            if (!_playerViewRaycaster.RaycastTrigger(out IInteractable interactable, _interactableLayer))
                return;
            
            interactable.Interact();
        }
    }
}