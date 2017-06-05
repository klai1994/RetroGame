using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Entities;

namespace Game.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float timeBeforeDestruction;
        [SerializeField] GameObject shooter;

        public float projectileSpeed = 1f;
        public float damage;
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
            }
            if (col.gameObject != shooter)
            {
                Destroy(gameObject);
            }
        }
    }
}