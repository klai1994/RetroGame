using UnityEngine;
using Game.Actors;
using Game.CameraUI.Dialogue;

namespace Game.Combat {
    public class CombatTesting : MonoBehaviour {

        public CombatData testData;

        void Start()
        {
            PlayerData.CreateNewPlayer("Combat Tester");

            // Test starting a battle from a script
            // BattleSystem.battleSystem.StartNewBattle(testData);
        }

    }
}