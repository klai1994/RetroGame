using UnityEngine;
using Game.Actors;

namespace Game.Dialogue
{
    [RequireComponent(typeof(ActorAvatar))]
    public class Interaction : MonoBehaviour
    {
        [SerializeField] DialogueEventName[] eventNames;
        [SerializeField] float interactionDistance = 2.5f;
        ActorAvatar avatar;
        PlayerAvatarControl player;
        int interactionIndex = 0;

        void Start()
        {
            PlayerAvatarControl.BroadcastPlayerInteraction += Interact;
            player = PlayerAvatarControl.GetPlayerInstance();
            avatar = GetComponent<ActorAvatar>();
        }

        public void Interact()
        {
            if (!player.InDialogue && avatar.GetDistance(player.gameObject) < interactionDistance)
            {
                player.InDialogue = true;
                player.GetActorAvatar().FaceDirection(transform.position);
                avatar.FaceDirection(player.GetActorAvatar().transform.position);

                DialogueControlHandler.InitializeEvent(eventNames[interactionIndex]);

                if (interactionIndex < eventNames.Length - 1)
                {
                    interactionIndex++;
                }
            }
        }

    }
}
