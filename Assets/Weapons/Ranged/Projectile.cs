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
        // TODO encapsulate
        public float projectileSpeed = 1f;
        public float damage;

        GameObject shooter;
        float timeCreated;

        void Start()
        {
            timeCreated = Time.time;
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