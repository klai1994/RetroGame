using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Game.CameraUI.Dialogue
{
    public class Letterbox : MonoBehaviour
    {
        [SerializeField] GameObject dialogueUIFrame;
        [SerializeField] Font font;
        [SerializeField] Text letterboxText;
        [SerializeField] Image characterPortrait;
        [SerializeField] bool staticDialogue = false;

        Sprite[] dialoguePortraits;
        AudioClip[] voices;
        AudioClip currentVoice;
        AudioSource audioSource;

        bool dialogueSkippable = true;
        public bool TextSegmentEnded { get; private set; }
        public float textSpeed = 0.5f;
        public int voiceFrequency = 3;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            dialoguePortraits = Resources.LoadAll<Sprite>("DialoguePortraits");
            voices = Resources.LoadAll<AudioClip>("Voices");
        }

        // Handles populating elements of letterbox
        public void ConfigureLetterbox(DialogueEventHolder dialogueEventHolder, int dialogueStage)
        {
            letterboxText.font = font;

            if (dialogueStage < dialogueEventHolder.eventInfoList.Count)
            {
                TextSegmentEnded = false;
                StartCoroutine(AnimateText(dialogueEventHolder.eventInfoList[dialogueStage].DialogueText));
            }
            else
            {
                DialogueControlHandler.currentEvent = null;
            }

            // While dialogue event is not finished
            if (dialogueStage < dialogueEventHolder.eventInfoList.Count)
            {
                dialogueUIFrame.SetActive(true);

                // If there is a portrait, sets and finds the correct one
                string portraitFile = dialogueEventHolder.eventInfoList[dialogueStage].characterPortrait;

                if (characterPortrait)
                {
                    if (portraitFile != "")
                    {
                        characterPortrait.gameObject.SetActive(true);
                        characterPortrait.sprite = QueryForPortrait(portraitFile);
                    }
                    else
                    {
                        characterPortrait.gameObject.SetActive(false);
                    }
                }

                // If there is a voice, sets and finds the correct one
                string voice = dialogueEventHolder.eventInfoList[dialogueStage].voice;
                if (audioSource)
                {
                    if (voice != "")
                    {
                        currentVoice = QueryForVoice(voice);
                        audioSource.clip = currentVoice;
                    }
                    else
                    {
                        currentVoice = null;
                    }
                }

            }
            else
            {
                if (!staticDialogue)
                {
                    dialogueUIFrame.SetActive(false);
                }
            }
        }

        // Checks resources folder for portrait in dialogue
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
        
        // Checks resources folder for voice audio in dialogue
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

        // Makes text appear one character at a time and plays voice sound, randomized to sound more natural
        public IEnumerator AnimateText(string text)
        {
            letterboxText.text = "";

            foreach (char letter in text)
            {
                if (Input.GetKey(KeyCode.X) && dialogueSkippable)
                {
                    break;
                }

                if (currentVoice != null)
                {
                    float rand = UnityEngine.Random.Range(0, voiceFrequency);
                    if (rand == 0)
                    {
                        audioSource.Play();
                    }
                }

                letterboxText.text += letter;
                yield return new WaitForSeconds(textSpeed);
            }

            letterboxText.text = text;
            TextSegmentEnded = true;
        }

    }
}