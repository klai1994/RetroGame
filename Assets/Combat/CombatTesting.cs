using UnityEngine;
using Game.Actors;
using Game.CameraUI.Dialogue;

namespace Game.Combat {
    public class CombatTesting : MonoBehaviour {

        public ActorStats enemyTestData;

        void Start()
        {
            PlayerData.GetPlayerData().Init("Combat Tester");
            // Test starting a battle from a script
            BattleSystem.battleSystem.StartNewBattle(enemyTestData);
        }

    }
}