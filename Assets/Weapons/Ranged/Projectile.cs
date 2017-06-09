using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Entities;

namespace Game.Weapons
{
    public class Projectile : MonoBehaviour
    {
        const float CONTACT_DESTRUCTION_DELAY = 0.1f;

        [SerializeField] float timeBeforeDestruction;
        [SerializeField] float projectileSpeed = 10f;
        [SerializeField] float damage = 10f;
        private float timeCreated;
        private Vector2 direction;

        public void AddDamageModifier(float damageModifier)
        {
            damage += damageModifier;
        }

        public void SetProjectileDirection(Vector2 direction)
        {
            this.direction = direction;
        }

        void Start()
        {
            timeCreated = Time.time;
            GetComponent<Rigidbody2D>().velocity += (direction * projectileSpeed);
        }

        void Update()
        {
            if (Time.time - timeCreated > timeBeforeDestruction)
            {
                Destroy(gameObject);
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            Enemy enemy;
            if (enemy = col.gameObject.GetComponent<Enemy>())
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject, CONTACT_DESTRUCTION_DELAY);
            }
        }
    }
}