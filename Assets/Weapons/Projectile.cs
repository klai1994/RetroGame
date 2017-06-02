using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float projectileSpeed = 1f;
    public float damage;
    [SerializeField] float timeBeforeDestruction;
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
        Enemy entity;
        if (entity = col.gameObject.GetComponent<Enemy>())
        {
            entity.TakeDamage(damage);
            Destroy(gameObject, 0.1f);
        }
    }
}
