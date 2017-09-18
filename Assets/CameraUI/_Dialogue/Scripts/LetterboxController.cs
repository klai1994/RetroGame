using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Game.CameraUI.Dialogue
{
    public class LetterboxController : MonoBehaviour
    {
        [SerializeField] GameObject dialogueUIFrame;
        [SerializeField] Font font;
        [SerializeField] Text letterboxText;
        [SerializeField] Image characterPortrait;
        [SerializeField] bool staticDialogue;

        Sprite[] dialoguePortraits;
        AudioClip[] voices;
        AudioClip currentVoice;
        AudioSource audioSource;

        static DialogueEventHolder currentEvent;
        public static DialogueEventHolder CurrentEvent
        {
            get
            {
                return currentEvent;
            }
            set
            {
                currentEvent = value;
                if (value != null)
                {
                    EventOccuring = true;
                }
                else
                {
                    dialogueLine = 0;
                    EventOccuring = false;
                }

            }
        }

        public static bool EventOccuring { get; private set; }
        public static bool dialogueSkippable = true;
        public bool TextSegmentEnded { get; private set; }

        static int dialogueLine = 0;
        public float textSpeed = 25f;
        public int voiceFrequency = 3;

        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            dialoguePortraits = Resources.LoadAll<Sprite>("DialoguePortraits");
            voices = Resources.LoadAll<AudioClip>("Voices");
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C) && EventOccuring && TextSegmentEnded)
            {
                ConfigureLetterbox();
            }
        }

        // Handles populating elements of letterbox
        public void ConfigureLetterbox()
        {
            letterboxText.font = font;
            TextSegmentEnded = false;

            // While dialogue event is not finished
            if (dialogueLine < CurrentEvent.eventInfoList.Count)
            {
                StartCoroutine(AnimateText(CurrentEvent.eventInfoList[dialogueLine].DialogueText));
                DressDialogue(dialogueLine);

                dialogueUIFrame.SetActive(true);
                dialogueLine++;
            }
            else
            {
                CurrentEvent = null;

                if (!staticDialogue)
                {
                    dialogueUIFrame.SetActive(false);
                }
            }
        }

        // If there is a portrait or voice to go with the dialogue stage, find and set them up
        void DressDialogue(int dialogueStage)
        {
            string portraitFile = CurrentEvent.eventInfoList[dialogueStage].characterPortrait;
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

            string voice = CurrentEvent.eventInfoList[dialogueStage].voice;
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
            // textSpeed is measured in milliseconds
            float textDelay = textSpeed / 1000;

            foreach (char letter in text)
            {
                if (Input.GetKey(KeyCode.X) && dialogueSkippable)
                {
                    letterboxText.text = text;
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
                yield return new WaitForSeconds(textDelay);
            }
            TextSegmentEnded = true;

        }

    }
}