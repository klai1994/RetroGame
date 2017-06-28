using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogue
{
    public class LetterboxManager : MonoBehaviour
    {
        [SerializeField] protected Text letterboxText;
        public float textSpeed = 0.5f;
        protected bool textSegmentEnded = false;

        public bool GetTextSegmentEnded()
        {
            return textSegmentEnded;
        }

        public virtual void ConfigurePanel(DialogueEventHolder dialogueEventHolder, int dialogueStage)
        {
            if (dialogueStage < dialogueEventHolder.dialogueEvents.Count)
            {
                textSegmentEnded = false;
                StartCoroutine(AnimateText(dialogueEventHolder.dialogueEvents[dialogueStage].dialogueText));
            }
            else
            {
                DialogueControlHandler.currentEvent = null;
            }
        }

        public virtual IEnumerator AnimateText(string text)
        {
            this.letterboxText.text = "";
            foreach (char letter in text)
            {
                // TODO remove if statement
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