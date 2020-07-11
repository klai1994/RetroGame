using Game.Actors;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Combat
{
    public class BattleSystem : MonoBehaviour
    {
        public static BattleSystem battleSystem;
        [SerializeField] GameObject battleUIFrame;
        [SerializeField] GameObject enemyParent;

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

        [SerializeField] Text playerHealth;
        [SerializeField] Text enemyHealth;

        const string PLAYER_TAG = "Player";
        ActorData playerInCombatData;
        ActorData enemyInCombatData;

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
                    BattleOccuring = false;
                }

                if (playerInCombatData.IsDead)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }

        }

        public void StartNewBattle(ActorData enemyData)
        {
            BattleOccuring = true;

            playerInCombatData = PlayerData.GetPlayerData();
            enemyInCombatData = ScriptableObject.Instantiate(enemyData);
            enemyInCombatData.Init(enemyData.ActorName, enemyData.BaseDamage, enemyData.MaxHealth);

            GameObject enemy = Instantiate(new GameObject(), enemyParent.transform);
            Image enemyImage = enemy.AddComponent<Image>();
            enemyImage.sprite = enemyData.CombatSprite;
            enemyImage.preserveAspect = true;

        }
    }
}