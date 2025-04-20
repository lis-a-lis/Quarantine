using UnityEngine;
using UnityEngine.UI;

namespace _Quarantine.Code.UI.HUD.InventoryHUD
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private Image _slotIcon;
        [SerializeField] private Image _itemIcon;
        [SerializeField] private Image _durabilitySliderIcon;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _unselectedColor;

        private readonly Color _invisibleColor = new Color(0, 0, 0, 0);
        private Color _currentColor;
        
        public void SetSlotSelectionStatus(bool isSelected, bool isFilled)
        {
            _currentColor = isSelected ? _selectedColor : _unselectedColor;
            
            _slotIcon.color = _currentColor;
            Debug.Log(_itemIcon.color);
            _itemIcon.color = isFilled ? _currentColor : _invisibleColor;
            Debug.Log(_itemIcon.color);
            _durabilitySliderIcon.color = _currentColor;
        }
        
        public void SetItemIcon(Sprite icon)
        {
            _itemIcon.sprite = icon;
        }
        
        public void SetItemDurability(float durabilityPercent)
        {
            _durabilitySliderIcon.fillAmount = durabilityPercent;
        }
        
        public void ClearView()
        {
            Debug.Log("Clear view");
            //SetSlotSelectionStatus(false);
            _durabilitySliderIcon.fillAmount = 0;
            _slotIcon.color = _unselectedColor;
            _itemIcon.sprite = null;
            _itemIcon.color = _invisibleColor;
            
        }
    }
}