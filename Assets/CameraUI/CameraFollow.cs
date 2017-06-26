using UnityEngine;

namespace Game.CameraUI
{
    public class CameraFollow : MonoBehaviour
    {
        // Screen height divded by pixels per unit sprites for consistent camera size
        private float worldUnitHeight = Screen.height / 16.0f;
        public int zoom = 1;

        [SerializeField] Transform target;
        public Transform Target
        {
            get; set;
        }

        // Use this for initialization
        void Start()
        {
            ChangeZoom(zoom);
        }
    
        // Update is called once per frame
        void Update()
        {
            if (target)
            {
                SetCameraPosition();
            }
        }

        private void SetCameraPosition()
        {
            transform.position = target.transform.position + new Vector3(0.0f, 0.0f, -10.0f);
        }

        public void ChangeZoom(int zoom)
        {
            switch (zoom)
            {
                case 0:
                    Camera.main.orthographicSize = (int)(worldUnitHeight / 8);
                    return;
                case 1:
                    Camera.main.orthographicSize = (int)(worldUnitHeight / 4);
                    return;
                default:
                    Camera.main.orthographicSize = (int)(worldUnitHeight / 2);
                    return;
            }
        }
    }
}