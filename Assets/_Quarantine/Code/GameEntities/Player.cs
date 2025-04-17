using System.Collections.Generic;
using UnityEngine;
using _Quarantine.Code.Input;
using _Quarantine.Code.FPSMovement;
using _Quarantine.Code.Infrastructure.PersistentProgress;

namespace _Quarantine.Code.GameEntities
{
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(PlayerFPSController))]
    //[RequireComponent(typeof(ItemPicker))]
    public class Player : MonoBehaviour, ISavable<PlayerSaveData>, ILoadable<PlayerSaveData>
    {
        public PlayerSaveData Save()
        {
            PlayerTransformSaveData transformation = new PlayerTransformSaveData(
                gameObject.transform.position,
                gameObject.transform.rotation,
                GetComponent<PlayerFPSController>().CameraRotation);
            
            PlayerInventorySaveData inventory = new PlayerInventorySaveData(
                -1, new List<ItemSaveData>());
            
            PlayerStatsSaveData stats = new PlayerStatsSaveData(
                100,
                100,
                100,
                100,
                100);

            PlayerSaveData data = new PlayerSaveData(transformation, inventory, stats);
            
            return data;
        }

        public void Load(PlayerSaveData data)
        {
            transform.position = data.transform.playerPosition;
            transform.rotation = data.transform.playerRotation;
            GetComponentInChildren<Camera>().transform.rotation = data.transform.cameraRotation;
        }
    }
}