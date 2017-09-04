using UnityEngine;
using Game.CameraUI;
using Game.CameraUI.Dialogue;

namespace Game.Actors
{
    [RequireComponent(typeof(Avatar))]
    public class Interaction : MonoBehaviour
    {
        [SerializeField] DialogueEventName[] eventNames;
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
            if (UIController.PlayerIsFree() && actor.GetDistance(player.gameObject) < interactionDistance)
            {
                UIController.SetPlayerInDialogue(true);
                player.GetActorAvatar().FaceDirection(transform.position);
                DialogueControlHandler.InitializeEvent(eventNames[interactionIndex]);

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
