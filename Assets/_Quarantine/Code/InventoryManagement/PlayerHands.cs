using UnityEngine;
using DG.Tweening;
using _Quarantine.Code.FPSMovement;
using _Quarantine.Code.Items.Implementation;

namespace _Quarantine.Code.InventoryManagement
{
    public class PlayerHands : MonoBehaviour
    {
        [SerializeField] private PlayerFPSController _playerFPSController;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Transform _itemHolder;
        [SerializeField] private Transform _itemAppearanceStartPosition;
        [SerializeField] private Transform _itemAppearanceEndPosition;
        [SerializeField] private float _itemAppearanceAnimationPathMagnitude = 0.25f;
        [SerializeField] private float _itemAppearanceAnimationDuration = 0.5f;
        
        private Item _itemInHands;
        private Sequence _itemShakingAnimation;
        private Sequence _itemAppearanceAnimation; 
        private Sequence _itemDisappearanceAnimation; 
        
        private void Awake()
        {
            _itemHolder.localPosition = _itemAppearanceStartPosition.localPosition;
            
            _itemShakingAnimation = DOTween.Sequence();
            _itemAppearanceAnimation = DOTween.Sequence();
            _itemDisappearanceAnimation = DOTween.Sequence();

            _itemShakingAnimation
                .Append(_itemHolder.DOShakeRotation(_itemAppearanceAnimationDuration, 1, 5))
                .SetLoops(-1)
                .SetAutoKill(false);
            
            _itemAppearanceAnimation
                .Append(_itemHolder.DOLocalMove(_itemAppearanceEndPosition.localPosition, _itemAppearanceAnimationDuration))
                .SetAutoKill(false);
            
            _itemDisappearanceAnimation
                .Append(_itemHolder.DOLocalMove(_itemAppearanceStartPosition.localPosition, _itemAppearanceAnimationDuration))
                .OnComplete(SetItemInvisibleInHands)
                .SetAutoKill(false);
        }

        private void OnEnable()
        {
            _inventory.SelectedSlotChanged += OnPlayerInventorySlotSelected;
            SetItemInvisibleInHands();
        }

        private void OnDisable()
        {
            _inventory.SelectedSlotChanged -= OnPlayerInventorySlotSelected;
        }

        private void OnDestroy()
        {
            _itemAppearanceAnimation.Kill();
            _itemDisappearanceAnimation.Kill();
            _itemShakingAnimation.Kill();
        }

        private void Update()
        {
            if (_playerFPSController.IsMoving)
                _itemShakingAnimation.Play();
            else
                _itemShakingAnimation.Pause();
        }

        private void OnPlayerInventorySlotSelected(int slotIndex)
        {
            if (_itemInHands != null)
                _itemDisappearanceAnimation.Restart();
            
            if (_inventory.IsSlotSelected)
                SetItemVisibleInHands(slotIndex);
        }

        private void SetItemVisibleInHands(int itemSlotIndexInInventory)
        {
            if (!_inventory.TryGetItem(itemSlotIndexInInventory, out Item item))
                return;
            
            _itemInHands = item;
            _itemInHands.GetComponent<Rigidbody>().isKinematic = true;
            _itemInHands.transform.SetParent(_itemHolder, false);
            _itemInHands.transform.localPosition = Vector3.zero;
            _itemInHands.transform.localRotation = Quaternion.identity;
            _itemInHands.gameObject.SetActive(true);
            _itemAppearanceAnimation.Restart();
        }

        private void SetItemInvisibleInHands()
        {
            if (_itemInHands == null)
                return;
            
            _itemInHands.gameObject.SetActive(false);
            _itemInHands = null;
        }
    }
}