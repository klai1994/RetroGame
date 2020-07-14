using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;

namespace Game.CameraUI.Dialogue
{
    public class DialogueSystem : MonoBehaviour
    {
        const string DIALOGUE_BOX_KEY = "DialogueBox";
        const string LETTER_BOX_KEY = "LetterBox";
        const string NAME_BOX_KEY = "NameBox";
        const string PORTRAIT_KEY = "PortraitBox";
        public static DialogueSystem dialogueSystem;
       
        [SerializeField] Font font = null;
        GameObject dialogueUIFrame;
        Text nameText;
        Text letterboxText;
        Image characterPortrait;

        Sprite[] dialoguePortraits;
        AudioClip[] voices;
        AudioClip currentVoice;
        AudioSource audioSource;

        DialogueEventHolder currentEvent;
        public DialogueEventHolder CurrentEvent
        {
            get
            {
                return currentEvent;
            }
            set
            {
                currentEvent = value;

                // If currentEvent has a value, there is one occuring. Otherwise, there is not.
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
        public bool EventOccuring { get; private set; }
        public bool dialogueSkippable = true;

        public bool TextSegmentEnded { get; private set; }
        public float textSpeed = 25f;
        public int voiceFrequency = 3;
     
        int dialogueLine = 0;

        void Awake()
        {
            if (dialogueSystem == null)
            {
                dialogueSystem = this;
            }
            else if (dialogueSystem != this)
            {
                Debug.LogError("There is more than once instance of DialogueSystem!");
            }

            // Grab the UI components in the scene
            dialogueUIFrame = GameObject.FindGameObjectWithTag(DIALOGUE_BOX_KEY);
            nameText = GameObject.FindGameObjectWithTag(NAME_BOX_KEY).GetComponent<Text>();
            letterboxText = GameObject.FindGameObjectWithTag(LETTER_BOX_KEY).GetComponent<Text>();
            characterPortrait = GameObject.FindGameObjectWithTag(PORTRAIT_KEY).GetComponent<Image>();
            audioSource = dialogueUIFrame.GetComponent<AudioSource>();

            // Prepare UI for scene
            letterboxText.font = font;
            nameText.font = font;
            dialogueUIFrame.SetActive(false);

            // Grab the portraits and voice files from the resources folder
            dialoguePortraits = Resources.LoadAll<Sprite>("DialoguePortraits");
            voices = Resources.LoadAll<AudioClip>("Voices");
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.C) && EventOccuring && TextSegmentEnded)
            {
                ConfigureLetterbox();
            }
        }

        public void InitiateDialogue(DialogueEventName dialogueEvent)
        {
            CurrentEvent = JsonReader.GetDialogueEvent(dialogueEvent);
            ConfigureLetterbox();
        }

        // Handles populating elements of letterbox
        void ConfigureLetterbox()
        {
            TextSegmentEnded = false;

            // While dialogue event is not finished
            if (dialogueLine < CurrentEvent.eventInfoList.Count)
            {
                dialogueUIFrame.SetActive(true);
                DressDialogue(dialogueLine);
                StartCoroutine(AnimateText(CurrentEvent.eventInfoList[dialogueLine].DialogueText));

                dialogueLine++;
            }
            else
            {
                CurrentEvent = null;
                dialogueUIFrame.SetActive(false);
            }
        }

        // If there is a portrait or voice to go with the dialogue stage, find and set them up
        void DressDialogue(int dialogueStage)
        {
            nameText.text = currentEvent.eventInfoList[dialogueStage].nameText;
            string portraitFile = CurrentEvent.eventInfoList[dialogueStage].characterPortrait;

            if (portraitFile != null && portraitFile != "")
            {
                characterPortrait.sprite = QueryForPortrait(portraitFile);
                characterPortrait.gameObject.SetActive(true);
            }
            else
            {
                characterPortrait.gameObject.SetActive(false);
            }

            string voiceFile = CurrentEvent.eventInfoList[dialogueStage].voice;
            if (audioSource)
            {
                if (voiceFile != null && voiceFile != "")
                {
                    currentVoice = QueryForVoice(voiceFile);
                    audioSource.clip = currentVoice;
                }
                else
                {
                    currentVoice = null;
                }
            }

        }

        // Checks resources folder for portrait in dialogue
        Sprite QueryForPortrait(string portraitFileName)
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
        AudioClip QueryForVoice(string voiceFileName)
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
        IEnumerator AnimateText(string text)
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