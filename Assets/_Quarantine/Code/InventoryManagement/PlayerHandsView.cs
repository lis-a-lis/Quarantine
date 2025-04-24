using DG.Tweening;
using UnityEngine;
using _Quarantine.Code.Items.Implementation;

namespace _Quarantine.Code.InventoryManagement
{
    public class PlayerHandsView : MonoBehaviour
    {
        [SerializeField] private Transform _itemHolder;
        [SerializeField] private Transform _animationStart;
        [SerializeField] private Transform _animationEnd;
        [SerializeField] private float _itemShakingOffset = 0.1f;

        private float _animationDuration = 0.5f;
        private Item _itemInHands;
        private Sequence _itemShakingAnimation;
        private Sequence _appearAnimation;
        private Sequence _disappearAnimation;
        

        private void Awake()
        {
            CreateAnimations();
        }

        private void CreateAnimations()
        {
            _itemShakingAnimation = DOTween.Sequence();
            _appearAnimation = DOTween.Sequence();
            _disappearAnimation = DOTween.Sequence();
            
            _appearAnimation
                .Append(_itemHolder.DOLocalMove(_animationEnd.localPosition, _animationDuration))
                .SetAutoKill(false);
            
            _disappearAnimation
                .Append(_itemHolder.DOLocalMove(_animationStart.localPosition, _animationDuration))
                .SetAutoKill(false);
        }

        public void SetItemInHands(Item item)
        {
            _itemInHands = item;
            _itemHolder.localPosition = _animationStart.localPosition;
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
            _itemHolder.localPosition = _animationStart.localPosition;
        }

        public void RunItemAppearAnimation(float duration)
        {
            _animationDuration = duration;
            _appearAnimation.Restart();
        }
        
        public void RunItemDisappearAnimation(float duration)
        {
            _animationDuration = duration;
            _disappearAnimation.Restart();
        }
    }
}