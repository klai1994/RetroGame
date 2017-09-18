using UnityEngine;
using Game.Actors;
using System.Collections.Generic;

namespace Game.CameraUI.Dialogue
{
    [RequireComponent(typeof(Avatar))]
    public class DialogueInteractionHandler : MonoBehaviour
    {
        [SerializeField] DialogueEventName[] eventNames;
        [SerializeField] float interactionDistance = 2.5f;
        DialogueEventName selectedEvent;

        int eventIndex;
        public int EventIndex
        {
            get { return eventIndex; }
            set
            {
                eventIndex = value;
                selectedEvent = eventNames[value];
            }
        }

        LetterboxController letterbox;
        Actor actor;
        PlayerAvatarControl player;

        void Start()
        {
            EventIndex = 0;
            actor = GetComponent<Actor>();
            player = PlayerAvatarControl.GetPlayerInstance();
            letterbox = FindObjectOfType<LetterboxController>();

            if (player)
            {
                player.BroadcastPlayerInteraction += Interact;
            }
        }

        void OnDestroy()
        {
            if (player)
            {
                player.BroadcastPlayerInteraction -= Interact;
            }
        }

        // Used for interactable dialogue in overworld, otherwise call InitiateDialogue directly
        void Interact()
        {
            if (PlayerAvatarControl.PlayerIsFree && actor.GetDistance(player.gameObject) < interactionDistance)
            {
                player.GetActorAvatar().FaceDirection(transform.position);
                // If interactable object is an avatar, face player
                if (actor.GetType() == typeof(ActorAvatar))
                {
                    ((ActorAvatar)actor).FaceDirection(player.GetActorAvatar().transform.position);
                }
                InitiateDialogue();
            }
        }

        public void InitiateDialogue()
        {
            LetterboxController.CurrentEvent = JsonReader.GetDialogueEvent(selectedEvent);
            letterbox.ConfigureLetterbox();

            // Goes to next dialogue event to be prompted on the next initializaion
            if (EventIndex < eventNames.Length - 1)
            {
                EventIndex++;
            }
        }

    }
}
