using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Entities;

namespace Game.Weapons
{
    public class Weapon : MonoBehaviour
    {

        [SerializeField] float damage = 10f;
            
        public float Damage
        {
            get; set;
        }

        public CircleCollider2D WeaponSwing
        {
            get; set;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            Enemy enemy;
            if (enemy = col.gameObject.GetComponent<Enemy>())
            {
                enemy.TakeDamage(damage);
            }
            Destroy(gameObject, 0.1f);
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, 1.5f);
        }
    }
}