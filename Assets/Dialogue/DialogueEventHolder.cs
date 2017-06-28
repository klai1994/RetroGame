using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogue
{
    public class DialogueEventHolder
    {

        public List<DialogueEventInfo> dialogueEvents;

    }

    public struct DialogueEventInfo
    {
        public string characterPortrait;
        public string dialogueText;
    }
}