using Game.Combat;
using UnityEngine;

namespace Game
{
    public class PlayerData : ActorData
    {
        const float STARTING_HEALTH = 60;
        const float INITIAL_DAMAGE = 5;

        static PlayerData playerData;
        public static PlayerData PlayerCombatData { get { return playerData; } private set { } }

        public static void CreateNewPlayer(string name)
        {
            playerData = ScriptableObject.CreateInstance<PlayerData>();
            PlayerCombatData.Init(name, INITIAL_DAMAGE, STARTING_HEALTH);
        }
    }
}
