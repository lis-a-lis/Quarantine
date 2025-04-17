using System;
using UnityEngine;

namespace _Quarantine.Code.Utils.CustomValues
{
    [Serializable]
    public struct ValueWithDuration
    {
        [SerializeField] private float _value;
        [SerializeField] private float _duration;
        
        public float Value => _value;
        public float Duration => _duration;
    }
}