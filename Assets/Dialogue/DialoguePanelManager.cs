using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using UnityEngine.UI;
using System;

namespace Game.Dialogue
{
    [RequireComponent(typeof(DialogueHandler))]
    public class DialoguePanelManager : LetterboxManager
    {
        private Sprite[] dialoguePortraits;

        [SerializeField] GameObject dialoguePanel;
        [SerializeField] Image characterPortrait;
        [SerializeField] Text characterNameText;

        void Awake()
        {
            dialoguePortraits = Resources.LoadAll<Sprite>("DialoguePortraits");
        }

        public override void ConfigurePanel(DialogueEventHolder dialogueEventHolder, int dialogueStage)
        {
            if (dialogueStage < dialogueEventHolder.dialogueEvents.Count)
            {
                textSegmentEnded = false;
                dialoguePanel.SetActive(true);

                characterPortrait.sprite = QueryForPortrait
                    (dialogueEventHolder.dialogueEvents[dialogueStage].characterPortrait);

                characterNameText.text = dialogueEventHolder.dialogueEvents[dialogueStage].characterName;

                StartCoroutine(AnimateText(dialogueEventHolder.dialogueEvents[dialogueStage].dialogueText));
            }
            else
            {
                DialogueHandler.currentEvent = null;
                dialoguePanel.SetActive(false);
            }
        }

        private Sprite QueryForPortrait(string portraitFileName)
        {

            foreach (Sprite portrait in dialoguePortraits)
            {
                if (portrait.name == portraitFileName)
                {
                    return portrait;
                }
            }
            throw new Exception("The specified portrait filename was not found.");
        }

        public override IEnumerator AnimateText(string text)
        {
            this.letterboxText.text = "";
            foreach (char letter in text)
            {
                if (Input.GetKey(KeyCode.X))
                {
                    break;
                }
                this.letterboxText.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }
            this.letterboxText.text = text;
            textSegmentEnded = true;
        }
    }
}