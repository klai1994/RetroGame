using UnityEngine;
using Game.CameraUI.Dialogue;
using Game.Combat;

namespace Game.Actors
{
    [RequireComponent(typeof(ActorAvatar))]
    public class PlayerAvatarControl : MonoBehaviour
    {
        public delegate void BroadcastInteraction();
        public event BroadcastInteraction BroadcastPlayerInteraction;

        const string HORIZONTAL_AXIS = "Horizontal";
        const string VERTICAL_AXIS = "Vertical";

        static bool playerIsFree;
        public static bool PlayerIsFree
        {
            get
            {
                if (!FindObjectOfType<PlayerAvatarControl>())
                {
                    playerIsFree = true;
                }
                return playerIsFree;
            }
            set
            {
                Rigidbody2D rbody = GetPlayerInstance().GetComponent<Rigidbody2D>();

                if (value == true)
                {
                    rbody.constraints = RigidbodyConstraints2D.None;
                    rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                }
                else
                {
                    rbody.constraints = RigidbodyConstraints2D.FreezePositionX 
                        | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                }

                playerIsFree = value;
            }
        }

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
            PlayerIsFree = !(DialogueSystem.dialogueSystem.EventOccuring || BattleSystem.battleSystem.BattleOccuring);

            if (Input.GetKeyDown(KeyCode.C) && BroadcastPlayerInteraction != null)
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