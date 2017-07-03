using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actors
{
    // TODO make static
    public class PlayerData : MonoBehaviour
    {

        static string playerName;
        public static string PlayerName
        {
            get
            {
                return playerName;
            }

            set
            {
                playerName = value;
            }
        }

    }
}
