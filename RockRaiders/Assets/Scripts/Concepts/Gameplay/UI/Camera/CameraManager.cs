using Assets.Scripts.Miscellaneous;
using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.UI.Camera
{
    public class CameraManager : Singleton<CameraManager>
    {
        private float _cameraForwardAngle = 0;

        public Vector3 Forward { get; private set; } = Vector3.forward;
        public Vector3 Right { get; private set; } = Vector3.right;
        public Vector3 Left { get; private set; } = Vector3.left;
        public Vector3 Back { get; private set; } = Vector3.back;
        public bool AllowCameraInteraction { get; set; } = true;

        public float YRotationAngle
        {
            get => _cameraForwardAngle;
            set
            {
                _cameraForwardAngle = value;
                Forward = Quaternion.AngleAxis(_cameraForwardAngle, Vector3.up) * Vector3.forward;
                Right = Quaternion.AngleAxis(_cameraForwardAngle + 90, Vector3.up) * Vector3.forward;
                Left = Quaternion.AngleAxis(_cameraForwardAngle - 90, Vector3.up) * Vector3.forward;
                Back = Quaternion.AngleAxis(_cameraForwardAngle + 180, Vector3.up) * Vector3.forward;
            }
        }
    }

}