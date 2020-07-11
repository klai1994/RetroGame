using System;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Game.Combat
{
    [CreateAssetMenu(menuName = "Combat/CombatData")]
    public class ActorData : ScriptableObject
    {
        [SerializeField] Sprite combatSprite = null;
        [SerializeField] string actorName = null;
        bool isDead = false;

        [SerializeField] float baseDamage;
        [SerializeField] float maxHealth;
        float currentHealth;
        public void Init(string actorName, float baseDamage, float maxHealth)
        {
            BaseDamage = baseDamage;
            MaxHealth = maxHealth;
            ActorName = actorName;
            currentHealth = maxHealth;
        }

        public Sprite CombatSprite { get { return combatSprite; } private set { } }
        public string ActorName
        {
            get
            {
                return actorName;
            }
            set
            {
                if (actorName != null)
                {
                    Debug.LogWarning($"Actor name changing from {actorName} to {value}!");
                }
                actorName = value;
                
            }
        }
        public bool IsDead { get { return isDead; } private set { } }

        public float BaseDamage { get { return baseDamage; } set { baseDamage = value; } }
        public float MaxHealth
        {
            get
            {
                return maxHealth;
            }
            set
            {
                if (value > 0)
                {
                    maxHealth = value;
                }
            }
        }
        public float CurrentHealth
        {
            get
            {
                return currentHealth;
            }
            set
            {
                if (value <= 0)
                {
                    isDead = true;
                    currentHealth = 0;
                }
                else
                {
                    currentHealth = value;
                }
            }
        }

        public float GetHealthPercentage()
        {
            return maxHealth / currentHealth;
        }
    
    }
}