using _Quarantine.Code.GameEntities;
using _Quarantine.Code.Interactable;
using _Quarantine.Code.InventoryManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using RadioSilence.Input;
using _Quarantine.Code.FPSMovement;

namespace _Quarantine.Code.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private Player _player;
        private InputActions _input;
        private IFPSController _fpsController;
        
        private void Awake()
        {
            _input = new InputActions();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            _player = GetComponent<Player>();
           
            _fpsController = _player.GetComponent<IFPSController>();
        }

        private void Update()
        {
            _fpsController.Move(_input.Player.Move.ReadValue<Vector2>());
            _fpsController.Look(_input.Player.Look.ReadValue<Vector2>());
        }

        private void OnEnable()
        {
            _input.Enable();
            
            _input.Player.Attack.performed += OnAttackPerformed;
            _input.Player.Attack.canceled += OnAttackCanceled;

            _input.Player.Sprint.performed += OnSprintPerformed;
            _input.Player.Sprint.canceled += OnSprintCanceled;
            
            _input.Player.FreeLook.performed += OnFreeLookPerformed;
            _input.Player.FreeLook.canceled += OnFreeLookCanceled;
        }

        private void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            Debug.Log("attack");
        }
        
        private void OnAttackCanceled(InputAction.CallbackContext obj)
        {
            
        }
        
        private void OnFreeLookPerformed(InputAction.CallbackContext obj)
        {
            _fpsController.SetCameraMode(CameraMode.Free);
        }

        private void OnFreeLookCanceled(InputAction.CallbackContext obj)
        {
            _fpsController.SetCameraMode(CameraMode.Fixed);
        }

        private void OnDisable()
        {
            _input.Disable();

            _input.Player.Sprint.performed -= OnSprintPerformed;
            _input.Player.Sprint.canceled -= OnSprintCanceled;
            
            _input.Player.FreeLook.performed -= OnFreeLookPerformed;
            _input.Player.FreeLook.canceled -= OnFreeLookCanceled;
        }

        private void OnSprintPerformed(InputAction.CallbackContext context)
        {
            _fpsController.StartSprint();
        }

        private void OnSprintCanceled(InputAction.CallbackContext context)
        {
            _fpsController.EndSprint();
        }
    }
}