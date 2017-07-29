using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Dialogue
{
    public class LetterboxManager : MonoBehaviour
    {
        [SerializeField] protected Text letterboxText;
        [SerializeField] protected Font font;
        protected bool textSegmentEnded = false;
        public float textSpeed = 0.5f;

        public bool GetTextSegmentEnded()
        {
            return textSegmentEnded;
        }

        public virtual void ConfigurePanel(DialogueEventHolder dialogueEventHolder, int dialogueStage)
        {
            letterboxText.font = font;

            if (dialogueStage < dialogueEventHolder.eventInfoList.Count)
            {
                textSegmentEnded = false;
                StartCoroutine(AnimateText(dialogueEventHolder.eventInfoList[dialogueStage].DialogueText));
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
                // TODO delete after demo
                if (Input.GetKey(KeyCode.X))
                {
                    break;
                }
                this.letterboxText.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }
            // TODO delete after demo
            this.letterboxText.text = text;
            textSegmentEnded = true;
        }
    }
}