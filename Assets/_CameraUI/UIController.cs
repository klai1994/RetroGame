using System.Collections.Generic;
using UnityEngine;
using Game.Actors;

namespace Game.CameraUI
{
    public class UIController : MonoBehaviour
    {
        static bool characterFrozen;
        static bool playerInDialogue = false;

        [SerializeField] GameObject inventory;
        float movementScale;

        void Start()
        {
            movementScale = ActorAvatar.MovementScale;
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

            foreach (ActorAvatar avatar in GameObject.FindObjectsOfType<ActorAvatar>())
            {
                avatar.ToggleAnimations();
            }
        }
    }
}