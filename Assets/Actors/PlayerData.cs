using Game.Combat;
using UnityEngine;

namespace Game
{
    public class PlayerData : ActorData
    {
        const float STARTING_HEALTH = 60;
        const float INITIAL_DAMAGE = 5;

        static PlayerData playerData;

        public static PlayerData GetPlayerData()
        {
            if (playerData == null)
            {
                playerData = ScriptableObject.CreateInstance<PlayerData>();
            }
            return playerData;
        }

        public void Init(string name)
        {
            playerData.Init(name, INITIAL_DAMAGE, STARTING_HEALTH);
        }
    }
}
