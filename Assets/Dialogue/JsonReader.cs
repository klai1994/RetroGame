using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;
using System.IO;

namespace Game.Dialogue
{
    public class JsonReader
    {
        public static Object[] dialogueEvents;

        static void LoadDialogueEvents()
        {
            dialogueEvents = Resources.LoadAll<Object>("DialogueEvents");
        }

        public static DialogueEventHolder ConvertJsonToDialogueEvent(int dialogueEventId)
        {
            LoadDialogueEvents();
            return JsonMapper.ToObject<DialogueEventHolder>(dialogueEvents[dialogueEventId].ToString());
        }

    }
}