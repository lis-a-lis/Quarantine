using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Quarantine.Code.UI.Loading
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private Image _progressBar;
        [SerializeField] private Color _contrastColor;

        private Sequence _loadingAnimation;

        private void Awake()
        {
            _loadingAnimation = DOTween.Sequence();

            _loadingAnimation
                .Append(_progressBar.transform.DOLocalRotate(new Vector3(0, 0, 360), 2, RotateMode.FastBeyond360)
                    .SetEase(Ease.Linear))
                .SetLoops(-1)
                .SetAutoKill(false);
        }

        public void UpdateProgressBar(float progress)
        {
            _progressBar.fillAmount = progress;
        }

        public void Show()
        {
            _loadingAnimation.Restart();
        }

        public void Hide()
        {
            _loadingAnimation.Kill();
        }
    }
}