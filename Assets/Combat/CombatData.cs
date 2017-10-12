﻿using UnityEngine;

namespace Game.Combat
{
    [CreateAssetMenu(menuName = "Combat/CombatData")]
    public class CombatData : ScriptableObject
    {
        [SerializeField] Sprite combatSprite;
        [SerializeField] string actorName;
        bool isDead = false;

        [SerializeField] float baseDamage;
        [SerializeField] float maxHealth;
        float currentHealth;

        public Sprite CombatSprite { get { return combatSprite; } private set { } }
        public string ActorName
        {
            get
            {
                return actorName;
            }
            set
            {
                if (actorName == null)
                {
                    actorName = value;
                }
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

        public void Init(string actorName, float baseDamage, float maxHealth)
        {
            BaseDamage = baseDamage;
            MaxHealth = maxHealth;
            ActorName = actorName;
            currentHealth = maxHealth;
        }
    
    }
}