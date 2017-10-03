namespace Game
{ 
    public class PlayerData
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
