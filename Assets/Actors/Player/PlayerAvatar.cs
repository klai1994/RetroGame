using System.Collections;
using UnityEngine;

namespace Game.Actors
{
    public class PlayerAvatar : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 1f;
        Rigidbody2D rbody;
        Animator animator;

        private const string ANIM_IS_WALKING = "isWalking";
        private const string MOVEMENT_X = "movement_x";
        private const string MOVEMENT_Y = "movement_y";

        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";

        bool startedDialogue;
        public bool InDialogue
        {
            get
            {
                return startedDialogue;
            }

            set
            {
                startedDialogue = value;
            }
        }

        public delegate void PlayerInteracted();
        public static event PlayerInteracted BroadcastPlayerInteraction;
        static PlayerAvatar playerInstance;

        public static PlayerAvatar GetPlayerInstance()
        {
            if (!playerInstance)
            {
                playerInstance = FindObjectOfType<PlayerAvatar>();
            }
            return playerInstance;
        }

        void Start()
        {
            rbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            // Starts the player facing down
            animator.SetFloat(MOVEMENT_Y, -1f);
            startedDialogue = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                BroadcastPlayerInteraction();
            }

            if (!InDialogue)
            {
                MovePlayer();
            }

            else
            {
                if (Game.Dialogue.DialogueControlHandler.currentEvent == null)
                {
                    startedDialogue = false;
                }
            }
        }

        private void SetAnimatorDirection(Vector2 direction)
        {
            animator.SetFloat(MOVEMENT_X, direction.x);
            animator.SetFloat(MOVEMENT_Y, direction.y);
        }

        private void MovePlayer()
        {
            Vector2 moveDirection = new Vector2(Input.GetAxisRaw(HORIZONTAL_AXIS), Input.GetAxisRaw(VERTICAL_AXIS));

            if (moveDirection != Vector2.zero)
            {
                animator.SetBool(ANIM_IS_WALKING, true);
                SetAnimatorDirection(moveDirection);
                rbody.MovePosition(rbody.position + moveDirection * movementSpeed);
            }
            else
            {
                rbody.velocity = Vector2.zero;
                animator.SetBool(ANIM_IS_WALKING, false);
            }
        }

        public void FaceDirection(Vector3 target)
        {
            Vector3 direction = (target - transform.position).normalized;

            animator.SetBool(ANIM_IS_WALKING, false);
            SetAnimatorDirection(direction);
        }
    }
}