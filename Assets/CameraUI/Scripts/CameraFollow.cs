using UnityEngine;

namespace Game.CameraUI
{
    public class CameraFollow : MonoBehaviour
    {
        // Screen height divded by pixels per unit sprites for consistent camera size
        int zoom = 6;
        public int Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                SetCameraZoom(value);
            }
        }

        public Transform Target { get; set; }

        // Use this for initialization
        void Start()
        {
            Target = Actors.PlayerAvatarControl.GetPlayerInstance().transform;
            SetCameraZoom(Zoom);
        }
       
        // Update is called once per frame
        void Update()
        {
            if (Target)
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
            transform.position = Target.transform.position + new Vector3(0.0f, 0.0f, -10.0f);
        }
    }
}