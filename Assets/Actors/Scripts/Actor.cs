using UnityEngine;
using Game.CameraUI.Dialogue;
using Game.Combat;

namespace Game.Actors
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Actor : MonoBehaviour
    {
        [SerializeField] DialogueEventName[] eventNames;
        [SerializeField] ActorData actorData;

        [SerializeField] float interactionDistance = 2.5f;
        bool canInteractWith = false;

        PlayerAvatarControl player;
        int eventIndex;
        public int EventIndex
        {
            get { return eventIndex; }
            set
            {
                // Goes to next dialogue event to be prompted on the next initializaion
                if (value <= eventNames.Length - 1)
                {
                    eventIndex = value;
                }

            }
        }

        void Start()
        {
            Init();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (actorData != null)
            {
                BattleSystem.battleSystem.StartNewBattle(actorData);
                Destroy(gameObject);
            }
        }

        void OnDestroy()
        {
            if (player)
            {
                player.BroadcastPlayerInteraction -= Interact;
            }
        }

        // Ensures subclasses can subscribe to the player input broadcast delegate
        protected void Init()
        {
            player = PlayerAvatarControl.GetPlayerInstance();
            canInteractWith = eventNames.Length > 0;

            if (canInteractWith)
            {
                AddInteraction(eventNames);
            }
        }

        public float GetDistance(GameObject target)
        {
            return Vector2.Distance(target.transform.position, transform.position);
        }

        public void AddInteraction(DialogueEventName[] eventNames)
        {
            player.BroadcastPlayerInteraction -= Interact;
            EventIndex = 0;
            player.BroadcastPlayerInteraction += Interact;
        }

        // Used for interactable dialogue in overworld, otherwise call DialogueSystem.InitiateDialogue directly
        void Interact()
        {
            if (PlayerAvatarControl.PlayerIsFree && GetDistance(player.gameObject) < interactionDistance)
            {
                player.GetActorAvatar().FaceDirection(transform.position);
                // If interactable object is an avatar, face player
                if (GetType() == typeof(ActorAvatar))
                {
                    ((ActorAvatar)this).FaceDirection(player.GetActorAvatar().transform.position);
                }

                DialogueSystem.dialogueSystem.InitiateDialogue(eventNames[EventIndex]);
                EventIndex++;
            }
        }

    }
}