using _Quarantine.Code.Stats;
using UnityEngine;
using UnityEngine.UI;

namespace _Quarantine.Code.UI.HUD.PlayerStatsHUD
{
    public class StatsHUDPresenter : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _mindBar;
        [SerializeField] private Image _satietyBar;
        [SerializeField] private Image _waterBar;
        private PlayerStats _stats;
        
        public void Initialize(PlayerStats playerStats)
        {
            _stats = playerStats;
            
            _stats.HealthChanged += UpdateView;
            _stats.MindChanged += UpdateView;
            _stats.SatietyChanged += UpdateView;
            _stats.WaterChanged += UpdateView;
        }

        private void OnDestroy()
        {
            _stats.HealthChanged -= UpdateView;
            _stats.MindChanged -= UpdateView;
            _stats.SatietyChanged -= UpdateView;
            _stats.WaterChanged -= UpdateView;
        }

        private void UpdateView(float value)
        {
            _healthBar.fillAmount = Mathf.Clamp01(_stats.Health / _stats.MaxHealth);
            _mindBar.fillAmount = Mathf.Clamp01(_stats.Mind / _stats.MaxHealth);
            _satietyBar.fillAmount =  Mathf.Clamp01(_stats.Satiety / _stats.MaxHealth);
            _waterBar.fillAmount =  Mathf.Clamp01(_stats.Water / _stats.MaxHealth);
        }
    }
}