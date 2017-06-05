using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Entities;

namespace Game.Weapons
{
    [CreateAssetMenu(menuName = ("Game/Weapon"))] 
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefab;
        [SerializeField] float weaponSwingRadius;

        public float damage;
        public Entity weaponUser;

        public void Hit()
        {
            damage = weaponUser.damage;
            CircleCollider2D collider = weaponPrefab.AddComponent<CircleCollider2D>();
            collider.radius = weaponSwingRadius;
            collider.isTrigger = true;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            Entity entity;
            if (entity = col.gameObject.GetComponent<Entity>())
            {
                entity.TakeDamage(damage);
            }
            Destroy(weaponPrefab, 0.1f);
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(weaponPrefab.transform.position, 1.5f);
        }
    }
}