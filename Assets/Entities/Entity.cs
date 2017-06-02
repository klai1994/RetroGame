using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Entity : MonoBehaviour {
    
    public enum Directions { Up, Down, Left, Right };

    [SerializeField] internal float maxHealth = 100f;
    [SerializeField] internal float currentHealth = 100f;
    [SerializeField] internal float damage;
    [SerializeField] internal float attackRange;
    [SerializeField] internal float attackDelay;

    internal Directions direction;
    internal Dictionary<Directions, Vector2> directionMagnitudes;

    public float HealthPercentage
    {
        get {
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
}
