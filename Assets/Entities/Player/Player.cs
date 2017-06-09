using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Weapons;

namespace Game.Entities
{
    public class Player : Entity, IDirectable
    {
        [SerializeField] Projectile projectilePrefab;
        [SerializeField] Weapon weaponPrefab;

        // Use this for initialization
        void Start()
        {
            currentHealth = maxHealth;
            rbody = GetComponent<Rigidbody2D>();
            SetUpDirections();
        }

        private void SetUpDirections()
        {
            directionMagnitudes = new Dictionary<Directions, Vector2>
            {
                { Directions.Left, Vector2.left },
                { Directions.Right, Vector2.right },
                { Directions.Down, Vector2.down },
                { Directions.Up, Vector2.up }
            };
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                PlayerShoot();
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                PlayerAttack();
            }

            MovePlayer();
            CheckDirectionFacing();
        }

        private void MovePlayer()
        {
            Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rbody.MovePosition(rbody.position + moveDirection * movementSpeed);
        }

        private void PlayerAttack()
        {
            // TODO implement attack
        }
         
        void PlayerShoot()
        {
            Projectile projectile;
            projectile = Instantiate(projectilePrefab, (transform.position +
                (Vector3)directionMagnitudes[direction]), Quaternion.identity);

            projectile.damage = damage;
            projectile.GetComponent<Rigidbody2D>().velocity +=
                directionMagnitudes[direction] * projectile.projectileSpeed;
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