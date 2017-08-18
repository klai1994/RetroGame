using UnityEngine;

namespace Game.Actors
{
    [RequireComponent(typeof(Avatar))]
    public class Interaction : MonoBehaviour
    {
        [SerializeField] Dialogue.DialogueEventName[] eventNames;
        [SerializeField] float interactionDistance = 2.5f;
        Actor actor;
        PlayerAvatarControl player;
        int interactionIndex = 0;

        void Start()
        {
            PlayerAvatarControl.BroadcastPlayerInteraction += Interact;
            player = PlayerAvatarControl.GetPlayerInstance();
            actor = GetComponent<Actor>();
        }

        public void Interact()
        {
            if (!player.InDialogue && !CameraUI.UIController.gamePaused 
                && actor.GetDistance(player.gameObject) < interactionDistance)
            {
                player.InDialogue = true;
                player.GetActorAvatar().FaceDirection(transform.position);

                Dialogue.DialogueControlHandler.InitializeEvent(eventNames[interactionIndex]);

                if (actor.GetType() == typeof(ActorAvatar))
                {
                    ((ActorAvatar)actor).FaceDirection(player.GetActorAvatar().transform.position);
                }

                if (interactionIndex < eventNames.Length - 1)
                {
                    interactionIndex++;
                }
            }
        }

    }
}
