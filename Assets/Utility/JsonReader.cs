using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;

namespace Game.Dialogue
{
    public class JsonReader
    {
        public static Object[] dialogueEvents;

        public static DialogueEventHolder ConvertJsonToDialogueEvent(int dialogueEventId)
        {
            if (dialogueEvents == null)
            {
                dialogueEvents = Resources.LoadAll<Object>("DialogueEvents");
            }

            return JsonMapper.ToObject<DialogueEventHolder>(dialogueEvents[dialogueEventId].ToString());
        }
    }
}