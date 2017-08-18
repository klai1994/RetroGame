using UnityEngine;
using Game.Actors;

namespace Game.CameraUI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] GameObject inventory;
        static ActorAvatar playerAvatar;
        public static bool gamePaused;

        void Start()
        {
           playerAvatar = PlayerAvatarControl.GetPlayerInstance().GetActorAvatar();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.X) && gamePaused)
            {
                gamePaused = false;
                Time.timeScale = 1;
                playerAvatar.ToggleAnimations();
                inventory.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.I) && !gamePaused)
            {
                PauseGame();
                inventory.SetActive(true);
            }
        }

        public static void PauseGame()
        {
            gamePaused = true;
            Time.timeScale = 0;
            playerAvatar.ToggleAnimations();
        }

    }
}