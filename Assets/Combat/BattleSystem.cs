using Game.Actors;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Combat
{
    public class BattleSystem : MonoBehaviour
    {
        const string BATTLE_FRAME_KEY = "BattleBox";
        const string ENEMY_FRAME_KEY = "EnemyBox";
        const string PLAYER_HEALTH_KEY = "PlayerHealthBox";

        public static BattleSystem battleSystem;
        GameObject battleUIFrame;
        GameObject enemyParent;

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

        [SerializeField] Text enemyHealth = null;
        Text playerHealth;

        /* Player and enemy stats are implemented in a more abstract fashion, being ScriptableObjects rather than GameObjects.
         * For enemy combat data, create a prefab with the ActorStats ScriptableObject in the project window. */
        ActorStats playerInCombatData;
        ActorStats enemyInCombatData;

        void Awake()
        {
            if (battleSystem == null)
            {
                battleSystem = this;
            }
            else if (battleSystem != this)
            {
                Debug.LogWarning("There is more than once instance of BattleSystem!");
            }

            enemyParent = GameObject.FindGameObjectWithTag(ENEMY_FRAME_KEY);
            battleUIFrame = GameObject.FindGameObjectWithTag(BATTLE_FRAME_KEY);
            playerHealth = GameObject.FindGameObjectWithTag(PLAYER_HEALTH_KEY).GetComponent<Text>();
            BattleOccuring = false;
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