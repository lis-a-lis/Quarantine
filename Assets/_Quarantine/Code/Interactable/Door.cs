using System;
using DG.Tweening;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace _Quarantine.Code.Interactable
{
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] private Quaternion _closedStateRotation;
        [SerializeField] private Quaternion _openedStateRotation;
        [SerializeField] private float _interactionDuration;
        
        private bool _isInteracting;
        private bool _isOpened;
        private Sequence _openingAnimation;
        private Sequence _closingAnimation;

        private void Awake()
        {
            _openingAnimation = DOTween.Sequence();
            _closingAnimation = DOTween.Sequence();

            _openingAnimation
                .Append(transform.DOLocalRotate(_openedStateRotation.eulerAngles, _interactionDuration))
                .SetAutoKill(false);
            
            _closingAnimation
                .Append(transform.DOLocalRotate(_closedStateRotation.eulerAngles, _interactionDuration))
                .SetAutoKill(false);
        }

        private void OnDestroy()
        {
            _openingAnimation.Kill();
            _closingAnimation.Kill();
        }

        public void Interact()
        {
            if (_isInteracting)
                return;
            
            RunInteraction().Forget();
        }
        
        private async UniTaskVoid RunInteraction()
        {
            _isInteracting = true;

            if (_isOpened)
            {
                _closingAnimation.Restart();
                
                await UniTask.WaitForSeconds(_interactionDuration);
                
                _isOpened = false;
            }
            else
            {
                _openingAnimation.Restart();
                
                await UniTask.WaitForSeconds(_interactionDuration);
                
                _isOpened = true;
            }
            
            _isInteracting = false;
        }
    }
}