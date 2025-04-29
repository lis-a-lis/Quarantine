using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Quarantine.Code.UI.Loading
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private Image _gearImage;
        [SerializeField] private Image _gearImage2;
        [SerializeField] private Image _gearImage3;
        [SerializeField] private Gradient _colorBlinkingEffectGradient;
        [SerializeField, Range(2, 10)] private float _animationLoopDuration = 2f;

        private Sequence _loadingAnimation;

        private void Awake()
        {
            _loadingAnimation = DOTween.Sequence();

            _loadingAnimation
                .Append(_gearImage.transform.DOLocalRotate(new Vector3(0, 0, 360),
                    2, RotateMode.FastBeyond360).SetEase(Ease.Linear))
                .Join(_gearImage2.transform.DOLocalRotate(new Vector3(0, 0, -360 * 1.5f),
                    2, RotateMode.FastBeyond360).SetEase(Ease.Linear))
                .Join(_gearImage3.transform.DOLocalRotate(new Vector3(0, 0, 360 * 2f), 
                    2, RotateMode.FastBeyond360).SetEase(Ease.Linear))
                .Join(_gearImage.DOGradientColor(_colorBlinkingEffectGradient, _animationLoopDuration))
                .Join(_gearImage2.DOGradientColor(_colorBlinkingEffectGradient, _animationLoopDuration - 0.5f))
                .Join(_gearImage3.DOGradientColor(_colorBlinkingEffectGradient, _animationLoopDuration - 1f))
                .SetLoops(-1)
                .SetAutoKill(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _loadingAnimation.Restart();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}