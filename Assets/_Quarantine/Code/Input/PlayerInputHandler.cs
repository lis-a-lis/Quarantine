using UnityEngine;
using UnityEngine.InputSystem;
using _Quarantine.Code.FPSMovement;
using _Quarantine.Code.GameEntities;
using _Quarantine.Code.InventoryManagement;

namespace _Quarantine.Code.Input
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private InputSystem_Actions _input;
        private PlayerEntity _playerEntity;
        private IFPSController _fpsController;
        private IInventoryInteractionsHandler _inventoryInteractionsHandler;
        
        private void Awake()
        {
            _input = new InputSystem_Actions();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            _playerEntity = GetComponent<PlayerEntity>();
           
            _fpsController = _playerEntity.GetComponent<IFPSController>();
            _inventoryInteractionsHandler = _playerEntity.GetComponent<IInventoryInteractionsHandler>();
        }

        public void LockInput()
        {
            _input.Disable();
        }

        public void UnlockInput()
        {
            _input.Enable();
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

            _input.Player.DropItem.performed += OnDropItemPerformed;

            _input.Player.SelectSlot1.performed += OnSlot1Selected;
            _input.Player.SelectSlot2.performed += OnSlot2Selected;
            _input.Player.SelectSlot3.performed += OnSlot3Selected;
            _input.Player.SelectSlot4.performed += OnSlot4Selected;
            _input.Player.SelectSlot5.performed += OnSlot5Selected;
        }

        private void OnDropItemPerformed(InputAction.CallbackContext obj)
        {
            _inventoryInteractionsHandler.DropItem();
        }

        private void OnSlot1Selected(InputAction.CallbackContext obj) => SelectSlot(1);

        private void OnSlot2Selected(InputAction.CallbackContext obj) => SelectSlot(2);

        private void OnSlot3Selected(InputAction.CallbackContext obj) => SelectSlot(3);

        private void OnSlot4Selected(InputAction.CallbackContext obj) => SelectSlot(4);

        private void OnSlot5Selected(InputAction.CallbackContext obj) => SelectSlot(5);
        
        private void SelectSlot(int slotIndex)
        {
            _inventoryInteractionsHandler.SelectSlot(slotIndex - 1);
        }

        private void OnAttackPerformed(InputAction.CallbackContext obj)
        {
            Debug.Log("attack");
            
            _inventoryInteractionsHandler.PickUpItem();
        }
        
        private void OnAttackCanceled(InputAction.CallbackContext obj)
        {
            
        }
        
        private void OnDisable()
        {
            _input.Disable();

            _input.Player.Attack.performed -= OnAttackPerformed;
            _input.Player.Attack.canceled -= OnAttackCanceled;
            
            _input.Player.Sprint.performed -= OnSprintPerformed;
            _input.Player.Sprint.canceled -= OnSprintCanceled;
            
            _input.Player.DropItem.performed -= OnDropItemPerformed;
            
            _input.Player.SelectSlot1.performed -= OnSlot1Selected;
            _input.Player.SelectSlot2.performed -= OnSlot2Selected;
            _input.Player.SelectSlot3.performed -= OnSlot3Selected;
            _input.Player.SelectSlot4.performed -= OnSlot4Selected;
            _input.Player.SelectSlot5.performed -= OnSlot5Selected;
            
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