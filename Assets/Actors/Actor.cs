using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actors
{
    public abstract class Actor : MonoBehaviour
    {
        [SerializeField] protected float maxHealth = 100f;
        [SerializeField] protected float currentHealth = 100f;
        [SerializeField] protected float baseDamage = 0;
        [SerializeField] protected float attackDelay = 1f; 
        [SerializeField] protected float movementSpeed = 1f;

        public enum Directions { Up, Down, Left, Right };
        protected const float DIRECTION_THRESHOLD = 0.2f;
        protected Directions direction;
        protected Dictionary<Directions, Vector2> directionMagnitudes;
        protected Rigidbody2D rbody;

        public abstract void CheckDirectionFacing();

        public float HealthPercentage
        {
            get
            {
                if (currentHealth > maxHealth) { return 1; }
                if (currentHealth < 0) { return 0; }
                else return currentHealth / maxHealth;
            }
        }

        public void TakeDamage(float damage)
        {
            currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void OnEnable()
        {
            directionMagnitudes = new Dictionary<Directions, Vector2>
            {
                { Directions.Left, Vector2.left },
                { Directions.Right, Vector2.right },
                { Directions.Down, Vector2.down },
                { Directions.Up, Vector2.up }
            };
            rbody = GetComponent<Rigidbody2D>();
            currentHealth = maxHealth;
        }

    }
}