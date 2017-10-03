using UnityEngine;
using UnityEngine.UI;

namespace Game.Combat
{
    public class BattleSystem : MonoBehaviour
    {
        public static BattleSystem battleSystem;
        [SerializeField] GameObject battleUIFrame;
        [SerializeField] CombatData playerCombatData;
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

        CombatData enemyData;

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
                enemyHealth.text = string.Format("Enemy: {0}/{1}", enemyData.CurrentHealth, enemyData.MaxHealth);
                playerHealth.text = string.Format("{0}: {1}/{2}", PlayerData.PlayerName, 60, 60);

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    enemyData.CurrentHealth -= 5;
                }

                if (enemyData.IsDead)
                {
                    BattleOccuring = false;
                }
            }

        }

        public void StartNewBattle(CombatData data)
        {
            BattleOccuring = true;
            enemyData = Instantiate(data, enemyParent.transform);

        }
    }
}