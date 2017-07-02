    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actors.Overworld
{
    public class NPC : Actor, IDirectable
    {
        private const string ANIM_IS_WALKING = "isWalking";
        private const string MOVEMENT_X = "movement_x";
        private const string MOVEMENT_Y = "movement_y";

        [SerializeField] float maxChaseDistance = 5;
        [SerializeField] float stoppingDistance = 0.1f;

        private float distanceToTarget;
        private GameObject target;
        public GameObject Target
        {
            get
            {
                return target;
            }

            set
            {
                target = value;
            }
        }

        void Start()
        {
            // TODO delete this line
            ChaseTarget(FindObjectOfType<Player>().gameObject);    
        }

        // Update is called once per frame
        void Update()
        {
            if (target)
            {
                GetDistanceToTarget();
                MoveToTarget();
                CheckDirectionFacing();
            }
        }

        private void MoveToTarget()
        {
            if (distanceToTarget < maxChaseDistance && distanceToTarget > stoppingDistance)
            {
                MoveNPC();
            }
            else
            {
                rbody.velocity = Vector2.zero;  
                animator.SetBool(ANIM_IS_WALKING, false);
            }
        }

        private void MoveNPC()
        {
            rbody.velocity = (Target.transform.position - transform.position) * movementSpeed;
            Vector2 moveDirection = rbody.velocity;

            if (moveDirection != Vector2.zero)
            {
                animator.SetBool(ANIM_IS_WALKING, true);
                animator.SetFloat(MOVEMENT_X, moveDirection.x);
                animator.SetFloat(MOVEMENT_Y, moveDirection.y);
            }
        }

        private void GetDistanceToTarget()
        {
            distanceToTarget = Vector2.Distance(Target.transform.position, transform.position);
        }

        public void ChaseTarget(GameObject target)
        {
            this.target = target;
        }

        public void StopChase()
        {
            this.target = gameObject;
        }

        // Checks based on movement
        public override void CheckDirectionFacing()
        {
            if (rbody.velocity.x < -DIRECTION_THRESHOLD)
            {
                direction = Directions.Left;
            }
            else if (rbody.velocity.x > DIRECTION_THRESHOLD)
            {
                direction = Directions.Right;
            }
            if (rbody.velocity.y < -DIRECTION_THRESHOLD)
            {
                direction = Directions.Down;
            }
            else if (rbody.velocity.y > DIRECTION_THRESHOLD)
            {
                direction = Directions.Up;
            }
        }
    }
}