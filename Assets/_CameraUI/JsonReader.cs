using UnityEngine;
using LitJson;

namespace Game.CameraUI.Dialogue
{
    public class JsonReader
    {
        public static Object[] dialogueEvents = Resources.LoadAll<Object>("DialogueEvents");

        public static DialogueEventHolder ConvertJsonToDialogueEvent(int dialogueEventId)
        {
            return JsonMapper.ToObject<DialogueEventHolder>(dialogueEvents[dialogueEventId].ToString());
        }
    }
}