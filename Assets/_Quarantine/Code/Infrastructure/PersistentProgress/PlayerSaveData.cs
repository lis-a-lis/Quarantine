using System;
using UnityEngine;

namespace _Quarantine.Code.Infrastructure.PersistentProgress
{
    [Serializable]
    public class PlayerTransformSaveData
    {
        public Vector3 playerPosition;
        public Quaternion playerRotation;
        public Quaternion cameraRotation;

        public PlayerTransformSaveData(Vector3 playerPosition, Quaternion playerRotation, Quaternion cameraRotation)
        {
            this.playerPosition = playerPosition;
            this.playerRotation = playerRotation;
            this.cameraRotation = cameraRotation;
        }
    }

    [Serializable]
    public class PlayerStatsSaveData
    {
        public float health;
        public float mindHealth;
        public float water;
        public float satiety;
        public float diseaseProgress;

        public PlayerStatsSaveData(float health, float mindHealth, float water, float satiety, float diseaseProgress)
        {
            this.health = health;
            this.mindHealth = mindHealth;
            this.water = water;
            this.satiety = satiety;
            this.diseaseProgress = diseaseProgress;
        }
    }
    
    [Serializable]
    public class PlayerSaveData
    {
        public PlayerTransformSaveData transform;
        public PlayerInventorySaveData inventory;
        public PlayerStatsSaveData stats;

        public PlayerSaveData(PlayerTransformSaveData transform, PlayerInventorySaveData inventory, PlayerStatsSaveData stats)
        {
            this.transform = transform;
            this.stats = stats;
            this.inventory = inventory;
        }
    }
}