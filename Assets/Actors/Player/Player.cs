using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actors
{
    public class Player : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 1f;
        Rigidbody2D rbody;
        Animator animator;

        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";
        private const string ANIM_IS_WALKING = "isWalking";
        private const string INPUT_Y = "input_y";
        private const string INPUT_X = "input_x";

        static string playerName;
        public static string PlayerName
        {
            get
            {
                return playerName;
            }

            set
            {
                playerName = value;
            }
        }
        bool startedDialogue;
        public bool StartedDialogue
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

        void Start()
        {
            rbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            // Starts the player facing down
            animator.SetFloat(INPUT_Y, -1);

            startedDialogue = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!StartedDialogue)
            {
                MovePlayer();
            }

            else
            {
                if (Game.Dialogue.DialogueControlHandler.currentEvent == null)
                {
                    startedDialogue = false;
                }

                animator.SetBool(ANIM_IS_WALKING, false);
            }
        }

        private void MovePlayer()
        {
            Vector2 moveDirection = new Vector2(Input.GetAxisRaw(HORIZONTAL_AXIS), Input.GetAxisRaw(VERTICAL_AXIS));

            if (moveDirection != Vector2.zero)
            {
                animator.SetBool(ANIM_IS_WALKING, true);
                animator.SetFloat(INPUT_X, moveDirection.x);
                animator.SetFloat(INPUT_Y, moveDirection.y);
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

            animator.SetFloat(INPUT_X, direction.x);
            animator.SetFloat(INPUT_Y, direction.y);
        }
    }
}