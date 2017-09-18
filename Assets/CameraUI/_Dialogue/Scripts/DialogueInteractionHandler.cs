using UnityEngine;
using Game.Actors;

namespace Game.CameraUI.Dialogue
{
    [RequireComponent(typeof(Avatar))]
    public class DialogueInteractionHandler : MonoBehaviour
    {
        [SerializeField] DialogueEventName[] eventNames;
        [SerializeField] float interactionDistance = 2.5f;

        public static DialogueEventHolder currentEvent;
        LetterboxController letterbox;

        Actor actor;
        PlayerAvatarControl player;

        int eventIndex = 0;
        int dialogueLine = 0;

        void Start()
        {
            actor = GetComponent<Actor>();
            player = PlayerAvatarControl.GetPlayerInstance();
            letterbox = FindObjectOfType<LetterboxController>();

            if (player)
            {
                player.BroadcastPlayerInteraction += Interact;
            }

        }

        // Used for interactable dialogue in overworld, otherwise call InitiateDialogue and ProgressDialogue directly
        void Interact()
        {
            if (actor.GetDistance(player.gameObject) < interactionDistance)
            {
                if (PlayerAvatarControl.PlayerIsFree)
                {
                    player.GetActorAvatar().FaceDirection(transform.position);
                    // If interactable object is an avatar, face player
                    if (actor.GetType() == typeof(ActorAvatar))
                    {
                        ((ActorAvatar)actor).FaceDirection(player.GetActorAvatar().transform.position);
                    }
                    InitiateDialogue();

                }
                else
                {
                    ProgressDialogue();
                }
            }

        }

        void OnDestroy()
        {
            player.BroadcastPlayerInteraction -= Interact;
        }

        public void InitiateDialogue()
        {
            DialogueEventName dialogueScene = eventNames[eventIndex];
            currentEvent = JsonReader.GetDialogueEvent(dialogueScene);
            dialogueLine = 0;
            letterbox.ConfigureLetterbox(currentEvent, dialogueLine);
            dialogueLine++;

            if (eventIndex < eventNames.Length - 1)
            {
                eventIndex++;
            }
        }

        public void ProgressDialogue()
        {
            if (letterbox.TextSegmentEnded)
            {
                letterbox.ConfigureLetterbox(currentEvent, dialogueLine);
                dialogueLine++;
            }
        }

    }
}
