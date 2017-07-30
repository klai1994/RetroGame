using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actors
{
    public class NPCAvatar : MonoBehaviour, IRangeable
    {
        [SerializeField] float movementSpeed = 1f;
        Rigidbody2D rbody;
        Animator animator;

        private const string ANIM_IS_WALKING = "isWalking";
        private const string MOVEMENT_X = "movement_x";
        private const string MOVEMENT_Y = "movement_y";

        [SerializeField] float maxChaseDistance = 5;
        [SerializeField] float stoppingDistance = 0.1f;
        [SerializeField] GameObject target;

        [SerializeField] bool isIdle = false;
        [SerializeField] float idleTurnRate = 3f;

        void Start()
        {
            rbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            animator.SetFloat(MOVEMENT_Y, -1f);      // Starts the NPC facing down

            if (isIdle)
            {
                StartCoroutine(IdleActions());
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (target)
            {
                MoveToTarget();
            }
        }

        private IEnumerator IdleActions()
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

        public float GetTargetDistance()
        {
            // TODO delete this object after player is a certain distance away
            return Vector2.Distance(target.transform.position, transform.position);
        }

        private void MoveToTarget()
        {
            if (GetTargetDistance() < maxChaseDistance && GetTargetDistance() > stoppingDistance)
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