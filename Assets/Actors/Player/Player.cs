using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Weapons;

namespace Game.Entities
{
    public class Player : Actor, IDirectable
    {
        [SerializeField] Projectile projectilePrefab;
        private float timeSinceLastHit;
        private Animator animator;

        public static string playerName;

        void Start()
        {
            animator = GetComponent<Animator>();
            
            // Starts the player facing down
            animator.SetFloat("input_y", -1);
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKey(KeyCode.V))
            {
                PlayerShoot();
            }

            MovePlayer();
            CheckDirectionFacing();
        }

        private void MovePlayer()
        {
            Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (moveDirection != Vector2.zero)
            {
                animator.SetBool("isWalking", true);
                animator.SetFloat("input_x", moveDirection.x);
                animator.SetFloat("input_y", moveDirection.y);
                rbody.MovePosition(rbody.position + moveDirection * movementSpeed);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }

        void PlayerShoot()
        {
            if (Time.time - timeSinceLastHit > attackDelay)
            {
                timeSinceLastHit = Time.time;
                Projectile projectile;
                projectile = Instantiate(projectilePrefab, (transform.position +
                    (Vector3)directionMagnitudes[direction]), Quaternion.identity);

                projectile.AddDamageModifier(baseDamage);
                projectile.SetProjectileDirection(directionMagnitudes[direction]);
            }
        }

        public override void CheckDirectionFacing()
        {
            if (Input.GetAxisRaw("Horizontal") < -DIRECTION_THRESHOLD)
            {
                direction = Directions.Left;
            }
            else if (Input.GetAxisRaw("Horizontal") > DIRECTION_THRESHOLD)
            {
                direction = Directions.Right;
            }
            else if (Input.GetAxisRaw("Vertical") < -DIRECTION_THRESHOLD)
            {
                direction = Directions.Down;
            }
            else if (Input.GetAxisRaw("Vertical") > DIRECTION_THRESHOLD)
            {
                direction = Directions.Up;
            }
        }
    }
}