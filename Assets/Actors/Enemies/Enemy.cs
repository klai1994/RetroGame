﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public class Enemy : Actor, IDirectable
    {
        [SerializeField] float aggroRange = 5;
        [SerializeField] float stoppingDistance = 0.1f;
        [SerializeField] float attackRange;
        [SerializeField] GameObject target;

        private float distanceToTarget;
        private bool isAttacking = false;

        void Start()
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update()
        {
            // If no target, prevents errors
            if (target == null)
            {
                target = gameObject;
            }

            GetDistanceToTarget();
            MoveToTarget();
            CheckDirectionFacing();
            CheckIfCanAttack();
        }

        private void GetDistanceToTarget()
        {
            distanceToTarget = Vector2.Distance(target.transform.position, transform.position);
        }

        private void CheckIfCanAttack()
        {
            if (distanceToTarget < attackRange && !isAttacking)
            {
                isAttacking = true;
                InvokeRepeating("Attack", 0, attackDelay);
            }
            else if (distanceToTarget > attackRange)
            {
                CancelInvoke();
                isAttacking = false;
            }
        }

        private void MoveToTarget()
        {
            if (distanceToTarget < aggroRange && distanceToTarget > stoppingDistance)
            {
                rbody.velocity = (target.transform.position - transform.position) * movementSpeed;
            }
            else
            {
                rbody.velocity = Vector2.zero;
            }
        }

        // Finds target's health and damages it
        void Attack()
        {
            Actor actor;
            if (actor = target.GetComponent<Actor>())
            {
                actor.TakeDamage(baseDamage);
            }
        }

        // Draw information on enemy distances
        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, stoppingDistance);
            Gizmos.DrawWireSphere(transform.position, aggroRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
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