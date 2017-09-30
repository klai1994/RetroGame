using UnityEngine;

namespace Game.Combat
{
    public class BattleSystem : MonoBehaviour
    {
        public static BattleSystem battleSystem;
        [SerializeField] GameObject battleUIFrame;

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

        public void StartNewBattle()
        {
            BattleOccuring = true;
        }
    }
}