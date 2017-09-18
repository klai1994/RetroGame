using UnityEngine;
using Game.CameraUI.Dialogue;

namespace Game.Actors
{
    [RequireComponent(typeof(ActorAvatar))]
    public class PlayerAvatarControl : MonoBehaviour
    {
        public delegate void BroadcastInteraction();
        public event BroadcastInteraction BroadcastPlayerInteraction;

        const string HORIZONTAL_AXIS = "Horizontal";
        const string VERTICAL_AXIS = "Vertical";

        public static bool PlayerIsFree { get; set; }

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
            PlayerIsFree = true;
        }

        // Update is called once per frame
        void Update()
        {
            PlayerIsFree = !(LetterboxController.EventOccuring);

            if (Input.GetKeyDown(KeyCode.C))
            {
                BroadcastPlayerInteraction();
            }

            avatar.MoveAvatar(new Vector2(Input.GetAxisRaw(HORIZONTAL_AXIS), Input.GetAxisRaw(VERTICAL_AXIS)));
         
        }

        public ActorAvatar GetActorAvatar()
        {
            return avatar;
        }

    }
}