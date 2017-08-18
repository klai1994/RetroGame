using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Game.CameraUI.Dialogue
{
    /// <summary>
    /// Attach this class to the SceneManager for general dialogue that may or may not use
    /// character portraits and where text is skippable. Use the LetterboxManager for more 
    /// cinematic dialogue.
    /// </summary>
    public class DialoguePanelManager : LetterboxManager
    {
        Sprite[] dialoguePortraits;
        [SerializeField] GameObject dialoguePanel;
        [SerializeField] Image characterPortrait;

        AudioClip[] voices;
        AudioClip currentVoice;
        AudioSource audioSource;

        public int voiceFrequency = 3;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            dialoguePortraits = Resources.LoadAll<Sprite>("DialoguePortraits");
            voices = Resources.LoadAll<AudioClip>("Voices");
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

                string voice = dialogueEventHolder.eventInfoList[dialogueStage].voice;
                if (voice != "")
                {
                    currentVoice = QueryForVoice(voice);
                    audioSource.clip = currentVoice;
                }
                else
                {
                    currentVoice = null;
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

        private AudioClip QueryForVoice(string voiceFileName)
        {
            foreach (AudioClip voice in voices)
            {
                if (voice.name == voiceFileName)
                {
                    return voice;
                }
            }
            throw new Exception("The specified voice filename was not found.");
        }

        public override IEnumerator AnimateText(string text)
        {
            this.letterboxText.text = "";
            float rand;

            foreach (char letter in text)
            {
                if (Input.GetKey(KeyCode.X))
                {
                    break;
                }
                this.letterboxText.text += letter;

                if (currentVoice != null)
                {
                    rand = UnityEngine.Random.Range(0, voiceFrequency);
                    if (rand == 0)
                    {
                        audioSource.Play();
                    }
                }

                yield return new WaitForSeconds(textSpeed);
            }
            this.letterboxText.text = text;
            textSegmentEnded = true;
        }
    }
}