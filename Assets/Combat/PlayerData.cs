using Game.Combat;
using UnityEngine;

namespace Game
{
    public static class PlayerData
    {
        const float STARTING_HEALTH = 60;
        const float INITIAL_DAMAGE = 5;

        static CombatData playerCombatData;
        public static CombatData PlayerCombatData { get { return playerCombatData; } private set { } }

        public static void CreateNewPlayer(string name)
        {
            playerCombatData = ScriptableObject.CreateInstance<CombatData>();
            PlayerCombatData.Init(name, INITIAL_DAMAGE, STARTING_HEALTH);
        }
    }
}
