using UnityEngine;
using UnityEngine.UI;

namespace _Quarantine.Code.UI.HUD.PlayerInventory
{
    public class SlotView : MonoBehaviour
    {
        [SerializeField] private Image _slotBorders;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private Image _durabilitySlider;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _unselectedColor;

        public void InitializeView()
        {
            _itemIcon.sprite = null;
            _itemIcon.gameObject.SetActive(false);
            SetSlotColor(_unselectedColor);
        }
        
        public void SetItemView(Sprite itemIcon)
        {
            _itemIcon.gameObject.SetActive(true);
            _durabilitySlider.fillAmount = 1f;
            _itemIcon.sprite = itemIcon;
        }

        public void ClearView()
        {
            _itemIcon.sprite = null;
            _itemIcon.gameObject.SetActive(false);
        }

        public void SetSlotSelected() => SetSlotColor(_selectedColor);

        public void SetSlotUnselected() => SetSlotColor(_unselectedColor);

        private void SetSlotColor(Color color)
        {
            _slotBorders.color = color;
            //_itemIcon.color = color;
        }
    }
}