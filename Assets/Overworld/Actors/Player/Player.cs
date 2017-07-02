using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actors.Overworld
{
    public class Player : Actor, IDirectable
    {
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
        bool isInDialogue;
        public bool IsInDialogue
        {
            get
            {
                return isInDialogue;
            }

            set
            {
                isInDialogue = value;
            }
        }

        [SerializeField] GameObject[] directionTriggers;
        Dictionary<Directions, GameObject> directionTriggerMap;


        void Start()
        {
            // Starts the player facing down
            animator.SetFloat(INPUT_Y, -1);

            IsInDialogue = false;
            directionTriggerMap = new Dictionary<Directions, GameObject>
            {
                {   Directions.Left, directionTriggers[0]  },
                {   Directions.Right, directionTriggers[1] },
                {   Directions.Up, directionTriggers[2]    },
                {   Directions.Down, directionTriggers[3]  },
            };
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsInDialogue)
            {
                TryStartDialogue();
                MovePlayer();
                CheckDirectionFacing();
            }

            else
            {
                animator.SetBool(ANIM_IS_WALKING, false);
            }
        }

        private void TryStartDialogue()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                directionTriggerMap[direction].SetActive(true);
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
                animator.SetBool(ANIM_IS_WALKING, false);
            }
        }

        public override void CheckDirectionFacing()
        {
            if (Input.GetAxisRaw(HORIZONTAL_AXIS) < -DIRECTION_THRESHOLD)
            {
                direction = Directions.Left;
            }
            else if (Input.GetAxisRaw(HORIZONTAL_AXIS) > DIRECTION_THRESHOLD)
            {
                direction = Directions.Right;
            }
            else if (Input.GetAxisRaw(VERTICAL_AXIS) < -DIRECTION_THRESHOLD)
            {
                direction = Directions.Down;
            }
            else if (Input.GetAxisRaw(VERTICAL_AXIS) > DIRECTION_THRESHOLD)
            {
                direction = Directions.Up;
            }
        }
    }
}