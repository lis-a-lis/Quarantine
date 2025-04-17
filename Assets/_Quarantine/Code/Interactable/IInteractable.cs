using System;
using UnityEngine;

namespace _Quarantine.Code.Interactable
{
    public interface IInteractable
    {
        public void Interact();
    }

    public interface IInteractionsHandler
    {
        public void Interact();
    }

    public class PlayerInteractionsHandler : MonoBehaviour, IInteractionsHandler
    {
        [SerializeField] private LayerMask _interactableObjectsLayer;
        [SerializeField] private float _interactDistance = 1.2f;
        
        public void Interact()
        {
            if (!Physics.Raycast(transform.position, transform.forward,
                    out RaycastHit hit, _interactableObjectsLayer)) return;
            if (hit.collider.gameObject.TryGetComponent(out IInteractable interactable))
                interactable.Interact();
            else
                throw new ArgumentException(hit.collider.gameObject.name + " is not a interactable");
        }
    }

    public interface IInteractor
    {
        public void TryInteract();
    }

    public class Interactor : IInteractor
    {
        public void TryInteract()
        {
            
        }
    }
}