using UnityEngine;

namespace _Quarantine.Code.FPSMovement
{
    public interface IFPSController
    {
        public void SetSensitivity(float sensitivity);
        public void SetCameraMode(CameraMode mode);
        public void Look(Vector3 lookDirection);
        
        public void Move(Vector2 movementDirection);
        public void StartSprint();
        public void EndSprint();
    }

    public enum CameraMode
    {
        Free,
        Fixed,
    }
}