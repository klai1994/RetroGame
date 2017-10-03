using UnityEngine;

namespace Game.Combat
{
    public class CombatData : MonoBehaviour
    {
        [SerializeField] float maxHealth;
        float currentHealth;
        public float MaxHealth { get { return maxHealth; } private set { } }
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

        bool isDead = false;
        public bool IsDead { get { return isDead; } private set { } }

        public float GetHealthPercentage()
        {
            return maxHealth / currentHealth;
        }

        void Awake()
        {
            currentHealth = maxHealth;
        }
    
    }
}