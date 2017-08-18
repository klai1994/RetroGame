using UnityEngine;
using System.Collections.Generic;

namespace Game.Dialogue
{
    public class DialogueEventAtlas : MonoBehaviour
    {
        static Dictionary<string, int> atlas = null;

        public static Dictionary<string, int> Atlas()
        {
            if (atlas == null)
            {
                atlas = new Dictionary<string, int>();

                for (int i = 0; i < JsonReader.dialogueEvents.Length; i++)
                {
                    atlas.Add(JsonReader.dialogueEvents[i].name, i);
                }
            }
            return atlas;
        }
    }

    // Values MUST correlate directly with files found in Resources/DialogueEvents
    public enum DialogueEventName
    {
        Prologue,

        #region Sandbox Events
        SandboxEvent1,
        SandboxEvent2,
        SandboxEvent3,
        SandboxEvent4,
        SandboxEvent5
        #endregion
    }
}