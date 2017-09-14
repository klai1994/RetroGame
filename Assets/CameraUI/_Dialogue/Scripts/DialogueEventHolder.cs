using System.Collections.Generic;

namespace Game.CameraUI.Dialogue
{
    /// <summary>
    /// This class is what .json dialogue event files are transformed into by LitJson. The list is the root node
    /// for each file, and each DialogueEventInfo struct represents a child node to the root node of the file.
    /// </summary>
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