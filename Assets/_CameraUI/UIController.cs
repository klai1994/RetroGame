using UnityEngine;
using Game.Actors;

namespace Game.CameraUI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] GameObject inventory;
        [SerializeField] AudioClip openMenuSound;
        AudioSource audioSource;

        static bool playerInDialogue = false;
        static bool characterFrozen;
        float movementScale;

        void Start()
        {
            movementScale = ActorAvatar.MovementScale;
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (!playerInDialogue)
            {
                if (Input.GetKeyDown(KeyCode.V))
                {
                    ToggleUI();
                    inventory.SetActive(!inventory.activeInHierarchy);

                    if (characterFrozen)
                    {
                        ActorAvatar.MovementScale = 0;
                    }
                    else
                    {
                        ActorAvatar.MovementScale = movementScale;
                    }
                }
            }

            if (!PlayerIsFree() && Dialogue.DialogueControlHandler.currentEvent == null)
            {
                playerInDialogue = false;
            }
        }

        public static void SetPlayerInDialogue(bool inDialogue)
        {
            playerInDialogue = inDialogue;
        }

        public static bool PlayerIsFree()
        {
            return !(playerInDialogue || characterFrozen);
        }

        private void ToggleUI()
        {
            characterFrozen = !characterFrozen;
            audioSource.PlayOneShot(openMenuSound);

            foreach (ActorAvatar avatar in GameObject.FindObjectsOfType<ActorAvatar>())
            {
                avatar.ToggleAnimations();
            }
        }
    }
}