using UnityEngine;

namespace _Quarantine.Code.UI.MainMenu.Screens
{
    [RequireComponent(typeof(RectTransform))]
    public class UIScreen : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform; 
    }
}
