using UnityEngine;

namespace _Quarantine.Code.Interactable
{
    public class Door : MonoBehaviour, IInteractable
    {
        private bool _isOpen = false;
        
        public void Interact()
        {
            if (_isOpen)
                Close();
            else
                Open();
        }

        private void Close()
        {
            throw new System.NotImplementedException();
        }

        private void Open()
        {
            throw new System.NotImplementedException();
        }
    }
}