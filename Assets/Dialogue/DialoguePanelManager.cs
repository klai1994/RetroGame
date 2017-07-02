using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using UnityEngine.UI;
using System;

namespace Game.Dialogue
{
    /// <summary>
    /// Attach this class to the SceneManager for general dialogue that may or may not use
    /// character portraits and where text is skippable. Use the LetterboxManager for more 
    /// cinematic dialogue.
    /// </summary>
    public class DialoguePanelManager : LetterboxManager
    {
        private Sprite[] dialoguePortraits;
        [SerializeField] GameObject dialoguePanel;
        [SerializeField] Image characterPortrait;

        void Awake()
        {
            dialoguePortraits = Resources.LoadAll<Sprite>("DialoguePortraits");
        }

        public override void ConfigurePanel(DialogueEventHolder dialogueEventHolder, int dialogueStage)
        {
            letterboxText.font = font;

            if (dialogueStage < dialogueEventHolder.eventInfoList.Count)
            {
                textSegmentEnded = false;
                dialoguePanel.SetActive(true);

                string portrait = dialogueEventHolder.eventInfoList[dialogueStage].characterPortrait;
                if (portrait != "")
                {
                    characterPortrait.gameObject.SetActive(true);
                    characterPortrait.sprite = QueryForPortrait(portrait);
                }
                else 
                {
                    characterPortrait.gameObject.SetActive(false);
                }

                StartCoroutine(AnimateText(dialogueEventHolder.eventInfoList[dialogueStage].DialogueText));
            }
            else
            {
                DialogueControlHandler.currentEvent = null;
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