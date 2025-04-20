using System;
using DG.Tweening;
using UnityEngine;

namespace _Quarantine.Code.FPSMovement
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerFPSController : MonoBehaviour, IFPSController
    {
        [SerializeField] private float _cameraSensitivity;
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector2 _cameraVerticalRotationLimits = new Vector2(-80, 80);
        [SerializeField] private Vector2 _cameraHorizontalRotationLimits = new Vector2(-90, 90);

        [SerializeField] private float _stepSpeed;
        [SerializeField] private float _sprintSpeed;
        [SerializeField] private float _sprintToStepCooldown;

        private CharacterController _characterController;

        private bool _isFreeCamera;
        private bool _isSprinting;
        private float _currentPlayerSpeed;
        private float _cameraVerticalRotation;
        private float _cameraHorizontalRotation;
        private Vector3 _cameraViewDirection;
        private Vector2 _playerMovementDirection;

        private Vector3 _cameraRotation;
        
        public Quaternion CameraRotation => _camera.transform.rotation;
        public bool IsMoving => _characterController.velocity != Vector3.zero;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            UpdatePlayerMoveSpeed();
            MovePlayer();
        }

        private void LateUpdate()
        {
            RotateCameraView();
        }

        public void SetSensitivity(float sensitivity)
        {
            if (sensitivity > 1 || sensitivity <= 0)
                throw new ArgumentException();
            
            _cameraSensitivity = sensitivity;
        }

        public void SetCameraMode(CameraMode mode)
        {
            if (_isFreeCamera && mode == CameraMode.Fixed)
                _camera.transform.DOLocalRotate(new Vector3(_cameraVerticalRotation, 0, 0), 0.5f);
                
            _isFreeCamera = mode == CameraMode.Free;
        }

        public void Move(Vector2 movementDirection)
        {
            _playerMovementDirection = movementDirection;
        }

        public void Look(Vector3 lookDirection)
        {
            _cameraViewDirection = lookDirection;
        }

        public void StartSprint()
        {
            _isSprinting = true;
            _currentPlayerSpeed = _sprintSpeed;
        }

        public void EndSprint()
        {
            _isSprinting = false;
            _currentPlayerSpeed = _stepSpeed;
        }

        private void UpdatePlayerMoveSpeed()
        {
            if (_playerMovementDirection == Vector2.zero)
            {
                _currentPlayerSpeed = 0;
                return;
            }

            _currentPlayerSpeed = _isSprinting ? _sprintSpeed : _stepSpeed;
        }

        private void MovePlayer()
        {
            Vector3 moveDirection = transform.forward * _playerMovementDirection.y
                + transform.right * _playerMovementDirection.x;

            Vector3 velocity = _currentPlayerSpeed * moveDirection + Physics.gravity;

            _characterController.Move(velocity * Time.deltaTime);
        }

        private float ClampCameraRotation(float rotationByAxis, Vector2 limits) =>
            Mathf.Clamp(rotationByAxis, limits.x, limits.y); 

        private void RotateCameraView()
        {
            _cameraVerticalRotation -= _cameraViewDirection.y * _cameraSensitivity;
            _cameraHorizontalRotation -= _cameraViewDirection.x * _cameraSensitivity;
            
            _cameraVerticalRotation = ClampCameraRotation(_cameraVerticalRotation, _cameraVerticalRotationLimits);
            _cameraHorizontalRotation = ClampCameraRotation(_cameraHorizontalRotation, _cameraHorizontalRotationLimits);
            
            if (!_isFreeCamera)
            {
                _cameraHorizontalRotation = 0;
                transform.Rotate(_cameraSensitivity * _cameraViewDirection.x * Vector3.up);
            }

            _camera.transform.localRotation = Quaternion.Euler(_cameraVerticalRotation, -_cameraHorizontalRotation, 0);
        }
    }
}