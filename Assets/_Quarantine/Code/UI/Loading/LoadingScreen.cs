using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Quarantine.Code.UI.Loading
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private Image _gearImage;
        [SerializeField] private Image _gearImage2;
        [SerializeField] private Image _gearImage3;
        [SerializeField] private Color _contrastColor;

        private Sequence _loadingAnimation;

        private void Awake()
        {
            _loadingAnimation = DOTween.Sequence();

            _loadingAnimation
                .Append(_gearImage.transform.DOLocalRotate(new Vector3(0, 0, 360), 2, RotateMode.FastBeyond360).SetEase(Ease.Linear))
                .Join(_gearImage2.transform.DOLocalRotate(new Vector3(0, 0, -360), 2, RotateMode.FastBeyond360).SetEase(Ease.Linear))
                .Join(_gearImage3.transform.DOLocalRotate(new Vector3(0, 0, 360), 2, RotateMode.FastBeyond360).SetEase(Ease.Linear))
                .SetLoops(-1)
                .SetAutoKill(false);
        }

        public void UpdateProgressBar(float progress)
        {
            _gearImage.fillAmount = progress;
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _loadingAnimation.Restart();
        }

        public void Hide()
        {
            //_loadingAnimation.Kill();
            gameObject.SetActive(false);
        }
    }
}