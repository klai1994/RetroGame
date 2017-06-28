using UnityEngine;

namespace Game.CameraUI
{
    public class CameraFollow : MonoBehaviour
    {
        // Screen height divded by pixels per unit sprites for consistent camera size
        [SerializeField] Transform target;
        [SerializeField] int initialZoom = 4;

        // Use this for initialization
        void Start()
        {
            SetCameraZoom(initialZoom);
        }
       
        // Update is called once per frame
        void Update()
        {
            if (target)
            {
                SetCameraPosition();
            }
        }

        public void SetCameraZoom(int zoomLevel)
        {
            int cameraZoom = Screen.height / 16 / zoomLevel;
            Camera.main.orthographicSize = cameraZoom;
        }

        private void SetCameraPosition()
        {
            transform.position = target.transform.position + new Vector3(0.0f, 0.0f, -10.0f);
        }
    }
}