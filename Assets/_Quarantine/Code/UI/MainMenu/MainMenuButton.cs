using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Quarantine.Code.UI.MainMenu
{
    [RequireComponent(typeof(Button))]
    public class MainMenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image _selectionIcon;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Color _selectionColor;
        [SerializeField] private float _selectionAnimationDuration;

        private Color _defaultColor;
        private Sequence _selectAnimation;
        private Sequence _deselectAnimation;

        private void Awake()
        {
            _defaultColor = _selectionIcon.color;
            _selectionIcon.gameObject.SetActive(false);
            _selectionIcon.color = new Color(_defaultColor.r, _defaultColor.g, _defaultColor.b, 0);
            
            InitializeAnimations();
        }

        private void InitializeAnimations()
        {
            _selectAnimation = DOTween.Sequence();
            _deselectAnimation = DOTween.Sequence();

            _selectAnimation
                .Append(_selectionIcon.DOColor(_selectionColor, _selectionAnimationDuration).SetEase(Ease.InCubic))
                .Join(_selectionIcon.DOFade(1, _selectionAnimationDuration).From(0).SetEase(Ease.InCubic))
                .Join(_text.DOColor(_selectionColor, _selectionAnimationDuration).SetEase(Ease.InCubic))
                .OnPlay(() => _selectionIcon.gameObject.SetActive(true))
                .SetAutoKill(false);


            _deselectAnimation
                .Append(_selectionIcon.DOColor(_selectionColor, _selectionAnimationDuration).SetEase(Ease.InCubic))
                .Join(_selectionIcon.DOFade(0, _selectionAnimationDuration).From(1).SetEase(Ease.InCubic))
                .Join(_text.DOColor(_defaultColor, _selectionAnimationDuration).SetEase(Ease.InCubic))
                .OnComplete(() => _selectionIcon.gameObject.SetActive(true))
                .SetAutoKill(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _selectAnimation.Restart();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _deselectAnimation.Restart();
        }
    }
}