using UnityEngine;
using UnityEngine.InputSystem;
using _Quarantine.Code.FPSMovement;
using _Quarantine.Code.GameEntities;

namespace _Quarantine.Code.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private PlayerEntity _playerEntity;
        private InputSystem_Actions _input;
        private IFPSController _fpsController;
        
        private void Awake()
        {
            _input = new InputSystem_Actions();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            _playerEntity = GetComponent<PlayerEntity>();
           
            _fpsController = _playerEntity.GetComponent<IFPSController>();
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
        }

        private void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            Debug.Log("attack");
            
            
        }
        
        private void OnAttackCanceled(InputAction.CallbackContext obj)
        {
            
        }
        
        private void OnDisable()
        {
            _input.Disable();

            _input.Player.Sprint.performed -= OnSprintPerformed;
            _input.Player.Sprint.canceled -= OnSprintCanceled;
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