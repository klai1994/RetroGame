using UnityEngine;
using LitJson;

namespace Game.CameraUI.Dialogue
{
    /// <summary>
    /// The JsonReader extracts the text from a specified dialogue event .json file and translates it to 
    /// a DialogueEventHolder struct.
    /// </summary>
    public class JsonReader
    {
        public static Object[] dialogueEvents = Resources.LoadAll<Object>("DialogueEvents");

        public static DialogueEventHolder ConvertJsonToDialogueEvent(int dialogueEventId)
        {
            return JsonMapper.ToObject<DialogueEventHolder>(dialogueEvents[dialogueEventId].ToString());
        }
    }
}