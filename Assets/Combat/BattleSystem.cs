using Game.Actors;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Combat
{
    public class BattleSystem : MonoBehaviour
    {
        [SerializeField] GameObject battleUIFrame = null;
        [SerializeField] Text enemyHealth = null;
        [SerializeField] Text playerHealth = null;
        [SerializeField] GameObject enemyParent = null;

        public static BattleSystem battleSystem;
        bool battleOccuring;
        public bool BattleOccuring
        {
            get
            {
                return battleOccuring;
            }
            set
            {
                battleOccuring = value;

                if (value == true)
                {
                    battleUIFrame.SetActive(true);
                }
                else
                {
                    battleUIFrame.SetActive(false);
                }

            }
        }

        /* Player and enemy stats are implemented in a more abstract fashion, being ScriptableObjects rather than GameObjects.
         * For enemy combat data, create a prefab with the ActorStats ScriptableObject in the project window. */
        ActorStats playerInCombatData;
        ActorStats enemyInCombatData;

        void Awake()
        {
            if (battleSystem == null)
            {
                battleSystem = this;
                battleOccuring = false;
            }
            else if (battleSystem != this)
            {
                Debug.LogWarning("There is more than once instance of BattleSystem!");
            }
        }

        void Update()
        {
            if (BattleOccuring)
            {
                enemyHealth.text = string.Format("{0}\n\n{1}/{2}",
                    enemyInCombatData.ActorName, enemyInCombatData.CurrentHealth, enemyInCombatData.MaxHealth);
                playerHealth.text = string.Format("{0}\n\n{1}/{2}",
                    playerInCombatData.ActorName, playerInCombatData.CurrentHealth, playerInCombatData.MaxHealth);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    enemyInCombatData.CurrentHealth -= playerInCombatData.BaseDamage;
                    playerInCombatData.CurrentHealth -= enemyInCombatData.BaseDamage;
                }

                if (enemyInCombatData.IsDead)
                {
                    PlayerData.GetPlayerData().CurrentHealth = playerInCombatData.CurrentHealth;
                    //TODO Add victory screen
                    BattleOccuring = false;
                }

                if (playerInCombatData.IsDead)
                {
                    // Restarts current level if player dies
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }

        }

        public void StartNewBattle(ActorStats enemyData)
        {
            BattleOccuring = true;

            // Set up player and enemy stats for battle
            playerInCombatData = PlayerData.GetPlayerData();
            enemyInCombatData = ScriptableObject.Instantiate(enemyData);
            enemyInCombatData.Init(enemyData.ActorName, enemyData.BaseDamage, enemyData.MaxHealth);

            // Shows enemy on screen
            GameObject enemy = Instantiate(new GameObject(), enemyParent.transform);
            Image enemyImage = enemy.AddComponent<Image>();
            enemyImage.sprite = enemyData.CombatSprite;
            enemyImage.preserveAspect = true;
        }
    }
}