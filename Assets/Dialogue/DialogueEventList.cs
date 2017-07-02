using UnityEngine;
using System.Collections.Generic;

namespace Game.Dialogue
{
    public class DialogueEventList : MonoBehaviour
    {
        public static HashSet<DialogueEventName> triggeredDialogue;

        void Start()
        {
            triggeredDialogue = new HashSet<DialogueEventName>();    
        }
    }

    // Values MUST correlate directly with files found in Resources/DialogueEvents
    public enum DialogueEventName
    {
        Prologue = 0,
        TestEvent = 1
    }
}