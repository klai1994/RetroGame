using System.Collections.Generic;

namespace Game.CameraUI.Dialogue
{
    public class DialogueEventHolder
    {
        // Name must match root node in .json file
        public List<DialogueEventInfo> eventInfoList;
    }

    public struct DialogueEventInfo
    {
        public string characterPortrait;
        public string dialogueText;
        public string voice;

        public string DialogueText
        {
            get
            {
                return dialogueText.Replace("@", Game.Actors.PlayerData.PlayerName);
            }
        }
    }
}