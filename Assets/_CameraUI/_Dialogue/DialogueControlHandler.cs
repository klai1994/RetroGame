using UnityEngine;

namespace Game.CameraUI.Dialogue
{
    /// <summary>
    /// This class provides the user the means to control progression of dialogue.
    /// </summary>
    [RequireComponent(typeof(LetterboxManager))]
    public class DialogueControlHandler : MonoBehaviour
    {
        public static DialogueEventHolder currentEvent;
        private static LetterboxManager manager;
        private static int dialogueStage;

        // Use this for initialization
        void Awake()
        {
            manager = gameObject.GetComponent<LetterboxManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C) && currentEvent != null && manager.GetTextSegmentEnded() == true)
            {
                ProgressDialogue();
            }
        }

        public static void InitializeEvent(DialogueEventName dialogueScene)
        {
            if (currentEvent == null)
            {
                int sceneIndex = DialogueEventAtlas.Atlas()[dialogueScene.ToString()];
                currentEvent = JsonReader.ConvertJsonToDialogueEvent(sceneIndex);

                dialogueStage = 0;
                ProgressDialogue();
            }
        }

        private static void ProgressDialogue()
        {
            manager.ConfigurePanel(currentEvent, dialogueStage);
            dialogueStage++;
        }
    }
}