using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity, IDirectable {

    [SerializeField] float aggroRange = 5;
    [SerializeField] float stoppingDistance = 0.1f;
    [SerializeField] float speed;

    private float distanceToTarget;
    private float threshold = 0.2f;
    private bool isAttacking = false;
    private GameObject target;
    private Rigidbody2D rbody;
    
    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // If no target, prevents errors
        if (target == null)
        {
            target = gameObject;
        }

        distanceToTarget = Vector2.Distance(target.transform.position, transform.position);

        // Move to target
        if (distanceToTarget < aggroRange && distanceToTarget > stoppingDistance)
        {
            rbody.velocity = (target.transform.position - transform.position) * speed;
        }
        else
        {
            rbody.velocity = Vector2.zero;
        }

        // Attack target
        if (distanceToTarget < attackRange && !isAttacking)
        {
            isAttacking = true;
            InvokeRepeating("Attack", 0, attackDelay);
        }
        else if (distanceToTarget > attackRange)
        {
            CancelInvoke();
            isAttacking = false;
        }
    }

    // Determine direction based on movement
    public Directions GetDirection()
    {
        if (rbody.velocity.x < -threshold)
        {
            return direction = Directions.Left;
        }
        else if (rbody.velocity.x > threshold)
        {
            return direction = Directions.Right;
        }
        if (rbody.velocity.y < -threshold)
        {
            return direction = Directions.Down;
        }
        else if (rbody.velocity.y > threshold)
        {
            return direction = Directions.Up;
        }
        else
        {
            return direction;
        }

    }

    // Finds target's health and damages it
    void Attack()
    {
        Entity entity;
        if (target != gameObject)
        {
            if (entity = target.GetComponent<Entity>())
            {
                entity.TakeDamage(damage);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
        Gizmos.DrawWireSphere(transform.position, aggroRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
