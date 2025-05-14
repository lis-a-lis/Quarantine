using System;
using UnityEngine;

namespace _Quarantine.Code.GameEntities.Courier
{
    public class MilitaryCourierSpawnPoint : MonoBehaviour
    {
        
    }
    
    public class MilitaryCourierMoveTarget : MonoBehaviour
    {
        
    }
    
    public class MilitaryCourier : MonoBehaviour
    {
        private MilitaryCourierSpawnPoint _spawnPoint;
        private MilitaryCourierMoveTarget _endTarget;
        
        private void Awake()
        {
            transform.position = _spawnPoint.transform.position;
        }
    }
}