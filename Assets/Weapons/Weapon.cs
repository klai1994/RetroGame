using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    [SerializeField] float damage = 10f;
    [SerializeField] float range = 2f;
    CircleCollider2D weaponSwing;
    GameObject weaponUser;

    public float Damage
    {
        get; set;
    }

    public CircleCollider2D WeaponSwing
    {
        get; set;
    }

    // TODO enable collider
    void OnTriggerEnter2D(Collider2D col)
    {
        Enemy entity;
        if (entity = col.gameObject.GetComponent<Enemy>())
        {
            entity.TakeDamage(damage);
        }
        // Destroy(gameObject, 0.1f);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 1.5f);
    }
}
