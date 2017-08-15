using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actors
{
    [RequireComponent(typeof(CameraUI.IsometricSpriteRenderer))]
    public class ActorAvatar : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 1.5f;
        Rigidbody2D rbody;
        Animator animator;

        const string HORIZONTAL_AXIS = "Horizontal";
        const string VERTICAL_AXIS = "Vertical";
        const string ANIM_IS_WALKING = "isWalking";
        
        const float MOVEMENT_SCALE = 0.10f;
        const string MOVEMENT_X = "movement_x";
        const string MOVEMENT_Y = "movement_y";

        [SerializeField] bool isIdle = false;
        [SerializeField] float idleTurnRate = 3f;

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
                rbody.MovePosition(rbody.position + moveDirection * (movementSpeed * MOVEMENT_SCALE));
            }
            else
            {
                animator.SetBool(ANIM_IS_WALKING, false);
            }
        }

        public IEnumerator IdleActions()
        {
            while (isIdle)
            {
                if (!PlayerAvatarControl.GetPlayerInstance().InDialogue)
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

        public void FaceDirection(Vector3 target)
        {
            animator.SetBool(ANIM_IS_WALKING, false);
            SetAnimatorDirection((target - transform.position).normalized);
        }

        public float GetDistance(GameObject target)
        {
            return Vector2.Distance(target.transform.position, transform.position);
        }

        void SetAnimatorDirection(Vector2 direction)
        {
            animator.SetFloat(MOVEMENT_X, direction.x);
            animator.SetFloat(MOVEMENT_Y, direction.y);
        }

    }
}