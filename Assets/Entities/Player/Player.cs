using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Weapons;

namespace Game.Entities
{
    public class Player : Entity, IDirectable
    {

        [SerializeField] float speed = 3f;
        [SerializeField] Projectile projectilePrefab;
        [SerializeField] Weapon weaponPrefab;

        private Rigidbody2D rbody;
        private float threshold = 0.2f;

        // Use this for initialization
        void Start()
        {
            currentHealth = maxHealth;
            rbody = GetComponent<Rigidbody2D>();
            directionMagnitudes = new Dictionary<Directions, Vector2>();
            directionMagnitudes.Add(Directions.Left, Vector2.left);
            directionMagnitudes.Add(Directions.Right, Vector2.right);
            directionMagnitudes.Add(Directions.Down, Vector2.down);
            directionMagnitudes.Add(Directions.Up, Vector2.up);
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
            GetDirection();
        }

        private void MovePlayer()
        {
            Vector2 moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rbody.MovePosition(rbody.position + moveDirection * speed);
        }

        // TODO refactor into Entity class and make weapon not damage self if used by enemies
        private void PlayerAttack()
        {
            Weapon weapon;
            weapon = Instantiate(weaponPrefab, (transform.position + 
                (Vector3)directionMagnitudes[direction]), Quaternion.identity);
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

        public Directions GetDirection()
        {
            if (Input.GetAxisRaw("Horizontal") < -threshold)
            {
                return direction = Directions.Left;
            }
            else if (Input.GetAxisRaw("Horizontal") > threshold)
            {
                return direction = Directions.Right;
            }
            else if (Input.GetAxisRaw("Vertical") < -threshold)
            {
                return direction = Directions.Down;
            }
            else if (Input.GetAxisRaw("Vertical") > threshold)
            {
                return direction = Directions.Up;
            }
            else
            {
                return direction;
            }
        }
    }
}