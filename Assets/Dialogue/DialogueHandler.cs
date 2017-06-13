using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogue
{
    public class DialogueHandler : MonoBehaviour
    {
        public static DialogueEventHolder currentEvent = null;
        private static int dialogueStage;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C) && currentEvent != null)
            {
                ProgressDialogue();
            }
        }

        public static void InitializeEvent(int dialogueScene)
        {
            currentEvent = JsonReader.ConvertJsonToDialogueEvent(dialogueScene);
            dialogueStage = 0;

            DialoguePanelManager.manager.ConfigurePanel(currentEvent, dialogueStage);
            dialogueStage++;
        }

        private void ProgressDialogue()
        {
            DialoguePanelManager.manager.ConfigurePanel(currentEvent, dialogueStage);
            dialogueStage++;
        }
    }
}