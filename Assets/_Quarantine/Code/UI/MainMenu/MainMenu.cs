using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using _Quarantine.Code.UI.MainMenu.Screens;
using _Quarantine.Code.Infrastructure.GameRequests;

namespace _Quarantine.Code.UI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private MainMenuScreen _menuScreen;
        [SerializeField] private SettingsScreen _settingsScreen;
        [SerializeField] private float _screenChangingDuration = 0.5f;

        private Sequence _openSettingsAnimation;
        private Sequence _openMainMenuAnimation;
        
        public event Action<MainMenuRequest> RequestSended;
        
        private void Awake()
        {
            InitializeAnimations();
            _settingsScreen.gameObject.SetActive(false);
        }
        

        private void InitializeAnimations()
        {
            _openSettingsAnimation = DOTween.Sequence();
            _openMainMenuAnimation = DOTween.Sequence();

            _openSettingsAnimation
                .Append(_settingsScreen.RectTransform.DOAnchorPosX(0, _screenChangingDuration))
                .Join(_menuScreen.RectTransform.DOAnchorPosX(-_menuScreen.RectTransform.sizeDelta.x, _screenChangingDuration))
                .OnPlay(() => _settingsScreen.gameObject.SetActive(true))
                .OnComplete(() => _menuScreen.gameObject.SetActive(false))
                .SetAutoKill(false);

            _openMainMenuAnimation
                .Append(_settingsScreen.RectTransform.DOAnchorPosX(_menuScreen.RectTransform.sizeDelta.x, _screenChangingDuration))
                .Join(_menuScreen.RectTransform.DOAnchorPosX(0, _screenChangingDuration))
                .OnPlay(() => _menuScreen.gameObject.SetActive(true))
                .OnComplete(() => _settingsScreen.gameObject.SetActive(false))
                .SetAutoKill(false);
        }

        public void OnPlayButtonPressed()
        {
            Debug.Log("Play button pressed");

            RequestSended?.Invoke(MainMenuRequest.Play);
        }

        public void OnSettingsButtonPressed()
        {
            Debug.Log("Settings button pressed");

            _openSettingsAnimation.Restart();
        }

        public void OnQuitButtonPressed()
        {
            Debug.Log("Quit button pressed");
            
            RequestSended?.Invoke(MainMenuRequest.Quit);
        }

        public void OnBackButtonPressed()
        {
            Debug.Log("Back button pressed");

            _openMainMenuAnimation.Restart();
        }
    }
}