using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actors
{
    public class WorldAvatar : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 1.5f;
        Rigidbody2D rbody;
        Animator animator;

        private const string HORIZONTAL_AXIS = "Horizontal";
        private const string VERTICAL_AXIS = "Vertical";
        private const string ANIM_IS_WALKING = "isWalking";

        // Reduction allows movementspeed to be a sensible number
        private const float MOVEMENT_REDUCTION = 0.10f;
        private const string MOVEMENT_X = "movement_x";
        private const string MOVEMENT_Y = "movement_y";

        [SerializeField] GameObject target;
        [SerializeField] bool isIdle = false;
        [SerializeField] float idleTurnRate = 3f;

        // Use this for initialization
        void Start()
        {
            rbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            // Starts avatar facing down
            animator.SetFloat(MOVEMENT_Y, -1f);

            if (isIdle)
            {
                StartCoroutine(IdleActions());
            }
        }

        public void MoveAvatar(Vector2 moveDirection)
        {
            if (moveDirection != Vector2.zero)
            {
                animator.SetBool(ANIM_IS_WALKING, true);
                SetAnimatorDirection(moveDirection);
                rbody.MovePosition(rbody.position + moveDirection * (movementSpeed * MOVEMENT_REDUCTION));
            }
            else
            {
                rbody.velocity = Vector2.zero;
                animator.SetBool(ANIM_IS_WALKING, false);
            }
        }

        public void SetTarget(GameObject target)
        {
            this.target = target;
        }

        public void ClearTarget()
        {
            this.target = gameObject;
        }

        public float GetTargetDistance()
        {
            return Vector2.Distance(target.transform.position, transform.position);
        }

        IEnumerator IdleActions()
        {
            while (isIdle)
            {
                if (!PlayerAvatar.GetPlayerInstance().InDialogue && rbody.velocity == Vector2.zero)
                {
                    animator.SetFloat(MOVEMENT_X, Random.Range(-1f, 1f));
                    animator.SetFloat(MOVEMENT_Y, Random.Range(-1f, 1f));
                    yield return new WaitForSeconds(Random.Range(1, idleTurnRate));
                }

                else
                {
                    yield return new WaitForEndOfFrame();
                }
            }

        }

        void SetAnimatorDirection(Vector2 direction)
        {
            animator.SetFloat(MOVEMENT_X, direction.x);
            animator.SetFloat(MOVEMENT_Y, direction.y);
        }

    }
}