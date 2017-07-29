using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actors
{
    public class NPCAvatar : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 1f;
        Rigidbody2D rbody;
        Animator animator;

        private const string ANIM_IS_WALKING = "isWalking";
        private const string MOVEMENT_X = "movement_x";
        private const string MOVEMENT_Y = "movement_y";

        [SerializeField] float maxChaseDistance = 5;
        [SerializeField] float stoppingDistance = 0.1f;
        public GameObject target;
        public bool isIdle = false;

        void Start()
        {
            rbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            // Starts the NPC facing down
            animator.SetFloat(MOVEMENT_Y, -1);
            ChaseTarget(target);

            StartCoroutine(IdleActions());
        }

        // Update is called once per frame
        void Update()
        {
            if (target)
            {
                GetDistanceToTarget();
                MoveToTarget();
            }
        }

        private IEnumerator IdleActions()
        {
            while (isIdle) {
                animator.SetBool(ANIM_IS_WALKING, true);
                animator.SetFloat(MOVEMENT_X, Random.Range(-1, 1));
                animator.SetFloat(MOVEMENT_Y, Random.Range(-1, 1));
                yield return new WaitForSeconds(Random.Range(0, 3));
            }

            animator.SetBool(ANIM_IS_WALKING, false);
            yield return null;
        }

        private void MoveToTarget()
        {
            if (GetDistanceToTarget() < maxChaseDistance && GetDistanceToTarget() > stoppingDistance)
            {
                rbody.velocity = (target.transform.position - transform.position) * movementSpeed;
                Vector2 moveDirection = rbody.velocity;

                if (moveDirection != Vector2.zero)
                {
                    animator.SetBool(ANIM_IS_WALKING, true);
                    animator.SetFloat(MOVEMENT_X, moveDirection.x);
                    animator.SetFloat(MOVEMENT_Y, moveDirection.y);
                }
            }
            else
            {
                rbody.velocity = Vector2.zero;  
                animator.SetBool(ANIM_IS_WALKING, false);
            }
        }

        private float GetDistanceToTarget()
        {
            return Vector2.Distance(target.transform.position, transform.position);
        }

        public void ChaseTarget(GameObject target)
        {
            this.target = target;
        }

        public void StopChase()
        {
            this.target = gameObject;
        }
    }
}