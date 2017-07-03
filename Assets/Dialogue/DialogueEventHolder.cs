using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogue
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

        public string DialogueText
        {
            get
            {
                return dialogueText.Replace("@", Game.Actors.Player.PlayerName);
            }
        }
    }
}