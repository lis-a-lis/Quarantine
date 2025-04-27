using UnityEngine;
using DG.Tweening;
using _Quarantine.Code.Items.Implementation;

namespace _Quarantine.Code.InventoryManagement
{
    public class PlayerHandsView : MonoBehaviour
    {
        [SerializeField] private Transform _itemHolder;
        [SerializeField] private Transform _positionInCameraVisibility;
        [SerializeField] private Transform _positionOutsideCameraVisibility;

        private float _animationDuration = 0.5f;
        private Item _itemInHands;
        private Sequence _appearAnimation;
        private Sequence _disappearAnimation;
        
        public bool IsItemInHands => _itemInHands != null;

        private void Awake()
        {
            CreateAnimations();
        }

        private void OnDestroy()
        {
            _appearAnimation.Kill();
            _disappearAnimation.Kill();
        }

        private void CreateAnimations()
        {
            _appearAnimation = DOTween.Sequence();
            _disappearAnimation = DOTween.Sequence();
            
            _appearAnimation
                .Append(_itemHolder.DOLocalMove(_positionInCameraVisibility.localPosition, _animationDuration))
                .SetAutoKill(false);
            
            _disappearAnimation
                .Append(_itemHolder.DOLocalMove(_positionOutsideCameraVisibility.localPosition, _animationDuration))
                .SetAutoKill(false);
        }

        public void SetItemInHands(Item item)
        {
            _itemHolder.localPosition = _positionOutsideCameraVisibility.localPosition;
            _itemInHands = item;
            _itemInHands.transform.SetParent(_itemHolder);
            _itemInHands.transform.localPosition = Vector3.zero;
            _itemInHands.transform.localRotation = Quaternion.identity;
            _itemInHands.gameObject.SetActive(true);
        }

        public void ClearItemInHands()
        {
            _itemInHands.transform.SetParent(null);
            _itemInHands.gameObject.SetActive(false);
            _itemInHands = null;
            _itemHolder.localPosition = _positionOutsideCameraVisibility.localPosition;
        }

        public void RunItemAppearAnimation(float duration) =>
            RunAnimationWithCustomDuration(_appearAnimation, duration);
        
        public void RunItemDisappearAnimation(float duration) =>
            RunAnimationWithCustomDuration(_disappearAnimation, duration);

        private void RunAnimationWithCustomDuration(Sequence animationSequence, float duration)
        {
            _animationDuration = duration;
            animationSequence.Restart();
        }
    }
}