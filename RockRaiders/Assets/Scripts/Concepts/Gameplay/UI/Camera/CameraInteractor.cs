using UnityEngine;

namespace Assets.Scripts.Concepts.Gameplay.UI.Camera
{
    public class CameraInteractor : MonoBehaviour
    {
        //private GameObject selectedGameObject;
        private const int LevelArea = 100;

        private const int ScrollAreaRadius = 15;
        private const float ScrollSpeed = 7.5f;
        private const int DragSpeed = 25;

        private const int ZoomSpeed = 25;
        private const int ZoomMin = 25;
        private const int ZoomMax = 100;

        private const float PanSpeed = 25;
        private const int PanAngleMin = 25;
        private const int PanAngleMax = 80;

        public float ControllerSensitivity = 0.5f;

        public CameraManager CameraManager = CameraManager.GetInstance();

        public Scripts.Map Map = Scripts.Map.GetInstance();

        private void Start()
        {
            
        }

        public float GetMousePanEasingFactor(float distanceToEdgeOfScreen)
        {
            if (distanceToEdgeOfScreen <= 0) distanceToEdgeOfScreen = 0;
            return 1 - (distanceToEdgeOfScreen / ScrollAreaRadius);
        }

        // Update is called once per frame
        private void Update()
        {
            if (!CameraManager.AllowCameraInteraction) return;

            // Init camera translation for this frame.
            var translation = Vector3.zero;

            // Zoom in or out
            var zoomDelta = Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed * Time.deltaTime;
            if (zoomDelta != 0)
            {
                translation -= Vector3.up * ZoomSpeed * zoomDelta;
            }

            // Start panning camera if zooming in close to the ground or if just zooming out.
            var pan = transform.eulerAngles.x - zoomDelta * PanSpeed;
            pan = Mathf.Clamp(pan, PanAngleMin, PanAngleMax);
            if (zoomDelta < 0 || transform.position.y < (ZoomMax / 2))
            {
                transform.eulerAngles = new Vector3(pan, CameraManager.YRotationAngle, 0);
            }

            // Move camera with arrow keys
            translation += (Input.GetAxis("Horizontal") * ControllerSensitivity * CameraManager.Right) + (Input.GetAxis("Vertical") * ControllerSensitivity * CameraManager.Forward);
            
            // Move camera if mouse pointer reaches screen borders
            if (Input.mousePosition.x < ScrollAreaRadius && transform.position.x > Map.Extents.min.x)
            {
                translation += CameraManager.Left * ScrollSpeed * Time.deltaTime * GetMousePanEasingFactor(Input.mousePosition.x);
            }

            if (Input.mousePosition.x >= Screen.width - ScrollAreaRadius && transform.position.x < Map.Extents.max.x)
            {
                translation += CameraManager.Right * ScrollSpeed * Time.deltaTime * GetMousePanEasingFactor(Screen.width - Input.mousePosition.x);
            }

            if (Input.mousePosition.y < ScrollAreaRadius && transform.position.z > Map.Extents.min.z)
            {
                translation += CameraManager.Back * ScrollSpeed * Time.deltaTime * GetMousePanEasingFactor(Input.mousePosition.y);
            }

            if (Input.mousePosition.y > Screen.height - ScrollAreaRadius && transform.position.z < Map.Extents.max.z)
            {
                translation += CameraManager.Forward * ScrollSpeed * Time.deltaTime * GetMousePanEasingFactor(Screen.height - Input.mousePosition.y);
            }

            // Keep camera within level and zoom area
            var desiredPosition = transform.position + translation;
            if (desiredPosition.x < -LevelArea || LevelArea < desiredPosition.x)
            {
                translation.x = 0;
            }
            if (desiredPosition.y < ZoomMin || ZoomMax < desiredPosition.y)
            {
                translation.y = 0;
            }
            if (desiredPosition.z < -LevelArea || LevelArea < desiredPosition.z)
            {
                translation.z = 0;
            }

            // Finally move camera parallel to world axis
            transform.position += translation;
        }
    }
}