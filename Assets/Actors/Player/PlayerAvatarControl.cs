using UnityEngine;
using Game.CameraUI;
using Game.CameraUI.Dialogue;

namespace Game.Actors
{
    [RequireComponent(typeof(ActorAvatar))]
    public class PlayerAvatarControl : MonoBehaviour
    {
        const string HORIZONTAL_AXIS = "Horizontal";
        const string VERTICAL_AXIS = "Vertical";

        public delegate void PlayerInteracted();
        public static event PlayerInteracted BroadcastPlayerInteraction;

        ActorAvatar avatar;
        static PlayerAvatarControl playerInstance;

        public static PlayerAvatarControl GetPlayerInstance()
        {
            if (!playerInstance)
            {
                playerInstance = FindObjectOfType<PlayerAvatarControl>();
            }
            return playerInstance;
        }

        // Use this for initialization
        void Awake()
        {
            avatar = GetComponent<ActorAvatar>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                BroadcastPlayerInteraction();
            }

            if (UIController.PlayerIsFree())
            {
                avatar.MoveAvatar(new Vector2(Input.GetAxisRaw(HORIZONTAL_AXIS), Input.GetAxisRaw(VERTICAL_AXIS)));
            }
        }

        public ActorAvatar GetActorAvatar()
        {
            return avatar;
        }

    }
}