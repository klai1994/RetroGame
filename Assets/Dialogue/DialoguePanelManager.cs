using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

using UnityEngine.UI;
using System;

namespace Game.Dialogue
{
    [RequireComponent(typeof(DialogueHandler))]
    public class DialoguePanelManager : MonoBehaviour
    {
        public static DialoguePanelManager manager;
        private Sprite[] dialoguePortraits;

        [SerializeField] GameObject dialoguePanel;
        [SerializeField] Image characterPortrait;
        [SerializeField] Text characterNameText;
        [SerializeField] Text dialogueText;
        [SerializeField] float textSpeed = 0.05f;

        void Awake()
        {
            if (!manager)
            {
                GameObject.DontDestroyOnLoad(gameObject);
                manager = this;
            }
            else if (manager != this)
            {
                Destroy(this);
            }

            dialoguePortraits = Resources.LoadAll<Sprite>("DialoguePortraits");
        }

        public void ConfigurePanel(DialogueEventHolder dialogueEventHolder, int dialogueStage)
        {
            if (dialogueStage < dialogueEventHolder.dialogueEvents.Count)
            {
                dialoguePanel.SetActive(true);

                characterPortrait.sprite = QueryForPortrait
                    (dialogueEventHolder.dialogueEvents[dialogueStage].characterPortrait);

                characterNameText.text = dialogueEventHolder.dialogueEvents[dialogueStage].characterName;
                StopCoroutine("AnimateText");
                StartCoroutine("AnimateText", dialogueEventHolder.dialogueEvents[dialogueStage].dialogueText);
            }
            else
            {
                DialogueHandler.currentEvent = null;
                dialoguePanel.SetActive(false);
            }

        }

        private IEnumerator AnimateText(string text)
        {
            dialogueText.text = "";
            foreach(char letter in text)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }
            yield return null;
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
    }
}