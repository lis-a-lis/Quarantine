using UnityEngine;
using _Quarantine.Code.Input;
using _Quarantine.Code.FPSMovement;
using _Quarantine.Code.InventoryManagement;
using _Quarantine.Code.Infrastructure.Services.SaveLoad;
using _Quarantine.Code.Infrastructure.PersistentProgress;

namespace _Quarantine.Code.GameEntities
{
    [RequireComponent(typeof(PlayerInputHandler))]
    [RequireComponent(typeof(PlayerFPSController))]
    [RequireComponent(typeof(PlayerInventory))]
    public class PlayerEntity : MonoBehaviour, ISavable<PlayerSaveData>, ILoadable<PlayerSaveData>, ISaveLoadEntity
    {
        public PlayerSaveData Save()
        {
            PlayerTransformSaveData transformation = new PlayerTransformSaveData(
                gameObject.transform.position,
                gameObject.transform.rotation,
                GetComponent<PlayerFPSController>().CameraRotation);
            
            PlayerInventory playerInventory = gameObject.GetComponent<PlayerInventory>();
            
            var slotsData = playerInventory.Save().slots;
            PlayerInventorySaveData inventory = new PlayerInventorySaveData(
                playerInventory.SelectedSlotIndex, slotsData);
            
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
            if (data == null)
                throw new System.ArgumentNullException(nameof(data));
            
            Debug.Log("PLAYER LOADING/");
            gameObject.transform.position = data.transform.playerPosition;
            gameObject.transform.rotation = data.transform.playerRotation;
            Debug.Log(data.transform.playerPosition);
            Debug.Log(data.transform.playerRotation);
            Debug.Log(data.transform.cameraRotation);
            Debug.Log("PLAYER DATA LOADED");
            
            Debug.Log(gameObject.transform.position);
            Debug.Log(gameObject.transform.rotation);
            
            
            gameObject.GetComponentInChildren<Camera>().transform.localRotation = data.transform.cameraRotation;
        }

        public void AcceptSave(ISavableEntitiesVisitor visitor)
        {
            visitor.SaveData(this);
        }
    }
}