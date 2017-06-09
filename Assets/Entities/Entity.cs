using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        internal const float DIRECTION_THRESHOLD = 0.2f;
        public enum Directions { Up, Down, Left, Right };

        [SerializeField] internal float maxHealth = 100f;
        [SerializeField] internal float currentHealth = 100f;
        [SerializeField] internal float baseDamage;
        [SerializeField] internal float attackDelay; 
        [SerializeField] internal float movementSpeed = 1f;

        internal Directions direction;
        internal Dictionary<Directions, Vector2> directionMagnitudes;
        internal Rigidbody2D rbody;

        public abstract void CheckDirectionFacing();

        public float HealthPercentage
        {
            get
            {
                if (currentHealth > maxHealth) { return 1; }
                if (currentHealth < 0) { return 0; }
                else
                    return currentHealth / maxHealth;
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
        }

    }
}