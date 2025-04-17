using UnityEngine;

namespace _Quarantine.Code.InventorySystem.Items.Behavior.Implementation
{
    public class MilitaryRadioStation : OldItem
    {
        [SerializeField] private Transform _antennaSlot;
        [SerializeField] private Transform _inOutDeviceSlot;
        
        [SerializeField] private float _signalReceptionRadius = 2000;
        [SerializeField] private int _minFrequency = 30000;
        [SerializeField] private int _maxFrequency = 80000;
        
        private int _currentFrequency = 50000;

        public void Activate()
        {
            
        }

        public void Deactivate()
        {
            
        }
        
        public void ChangeFrequency(int frequency)
        {
            _currentFrequency = Mathf.Clamp(frequency, _minFrequency, _maxFrequency);
        }
        
        public void AttachAntenna()
        {
            
        }

        public void AttachBattery()
        {
            
        }

        public void AttachInOutDevice()
        {
            
        }
    }
}