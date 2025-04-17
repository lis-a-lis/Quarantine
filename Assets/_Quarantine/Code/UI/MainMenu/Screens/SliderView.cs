using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Quarantine.Code.UI.MainMenu.Screens
{
    public class SliderView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _valueText;

        private int _value;

        public int Value => _value;

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(float value)
        {
            _value = Mathf.Clamp((int)(value * 100), 1, 100);
            _valueText.text = _value.ToString();
        }
    }
}