using System;
using TMPro;
using UnityEngine;

namespace _Quarantine.Code.UI.HUD.DialogueHUD
{
    public class DialogueTextView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textView;
        private string _text;

        public event Action<string> Selected;
        
        public void SetText(string text)
        {
            _text = text;
            _textView.text = text;
        }

        public void OnDialogueSelected()
        {
            Selected?.Invoke(_text);
        }
    }
}