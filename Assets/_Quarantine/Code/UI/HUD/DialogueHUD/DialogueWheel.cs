using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Quarantine.Code.UI.HUD.DialogueHUD
{
    public class DialogueWheel : MonoBehaviour
    {
        [SerializeField] private DialogueTextView _viewPrefab;
        [SerializeField] private float _radius;

        private List<DialogueTextView> _views = new List<DialogueTextView>();
        private List<string> _selectedDialogues = new List<string>();
        private int _maxSelectionsAmount;
        private int _selectionsAmount;

        public event Action<string[]> DialogueFinished;
        
        public void ShowWheel(string[] texts, int maxSelectionsAmount)
        {
            _maxSelectionsAmount = maxSelectionsAmount;
            
            float angleBetweenTexts = 360 / texts.Length;
            float angle = 0;
            
            foreach (var text in texts)
            {
                DialogueTextView view = Instantiate<DialogueTextView>(_viewPrefab, transform, false);
                _views.Add(view);
                view.SetText(text);
                Vector3 offset = Quaternion.Euler(0, 0, angle) 
                                 * Vector2.up * _radius;

                view.transform.position = transform.position + offset;
                view.Selected += SelectDialogue;
                
                angle += angleBetweenTexts;
            }
        }

        private void SelectDialogue(string dialogueText)
        {
            _selectedDialogues.Add(dialogueText);
            _selectionsAmount++;
            
            if (_selectionsAmount == _maxSelectionsAmount)
                FinishDialogue();
        }

        private void FinishDialogue()
        {
            DialogueFinished?.Invoke(_selectedDialogues.ToArray());

            foreach (DialogueTextView view in _views)
            {
                view.gameObject.SetActive(false);
                view.Selected -= SelectDialogue;
                Destroy(view.gameObject);
            }
            
            _views.Clear();
            _selectedDialogues.Clear();
            _selectionsAmount = 0;
            _maxSelectionsAmount = 0;
        }
    }
}