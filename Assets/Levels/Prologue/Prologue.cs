using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.Dialogue;
using System;
using UnityEngine.SceneManagement;

namespace Game.Levels.Prologue
{
    public class Prologue : MonoBehaviour
    {

        // Refer to ASCII table for char values 
        const int LETTER_GRID_X = 7;
        const int LETTER_GRID_Y = 8;
        const int MAX_CHARACTER_LENGTH = 13;
        const string SELECT_TRIGGER = "Select";

        [SerializeField] Image fadePanel;
        [SerializeField] Text letterPrefab;
        [SerializeField] GameObject letterGrid;
        [SerializeField] GameObject letterBox;

        [SerializeField] float fadeIncrement;
        [SerializeField] float fadeDelay;
        [SerializeField] int introSceneId;
        bool sceneStarted = false;

        Text[,] textGrid;
        [SerializeField] Text username;
        [SerializeField] GameObject nameSelect;

        [SerializeField] GameObject arrow;
        [SerializeField] Vector3 arrowOffset;
        [SerializeField] Animator arrowAnimator;
        [SerializeField] AudioClip[] clip;
        AudioSource audioSource;
        Vector2 arrowSelectIndex;
        Dictionary<SelectionSounds, int> selectionSounds;

        const float KEY_DELAY = 0.2f;
        float timePassedSinceKey = 0;

        enum SelectionSounds
        {
            Select,
            CannotSelect,
            Move,
            Confirm
        }

        // Use this for initialization
        void Start()
        {
            selectionSounds = new Dictionary<SelectionSounds, int> {
                { SelectionSounds.Select, 0 },
                { SelectionSounds.CannotSelect, 1 },
                { SelectionSounds.Move, 2 },
                { SelectionSounds.Confirm, 3 }
            };
            audioSource = GetComponent<AudioSource>();
            arrowSelectIndex = Vector2.zero;
            textGrid = new Text[LETTER_GRID_X, LETTER_GRID_Y];
            PopulateLetterGrid();
        }

        void Update()
        {
            if (sceneStarted == true && DialogueControlHandler.currentEvent == null)
            {
                sceneStarted = !sceneStarted;
                StartCoroutine(EndScene());
            }
            Text selectedLetter = SelectLetter();
            timePassedSinceKey += Time.deltaTime;

            if (nameSelect.activeInHierarchy)
            {
                // Y values are reversed because the grid opens downwards
                if (Input.GetKey(KeyCode.W) && timePassedSinceKey > KEY_DELAY)
                {
                    if (arrowSelectIndex.y > 0) arrowSelectIndex.y -= 1;
                    else arrowSelectIndex.y = LETTER_GRID_Y - 1;
                    FinishArrowMove();
                }
                else if (Input.GetKey(KeyCode.S) && timePassedSinceKey > KEY_DELAY)
                {
                    if (arrowSelectIndex.y < LETTER_GRID_Y - 1) arrowSelectIndex.y += 1;
                    else arrowSelectIndex.y = 0;
                    FinishArrowMove();
                }
                else if (Input.GetKey(KeyCode.D) && timePassedSinceKey > KEY_DELAY)
                {
                    if (arrowSelectIndex.x < LETTER_GRID_X - 1) arrowSelectIndex.x += 1;
                    else
                    {
                        arrowSelectIndex.x = 0;
                        if (arrowSelectIndex.y < LETTER_GRID_Y - 1) arrowSelectIndex.y += 1;
                    }
                    FinishArrowMove();
                }
                else if (Input.GetKey(KeyCode.A) && timePassedSinceKey > KEY_DELAY)
                {
                    if (arrowSelectIndex.x > 0) arrowSelectIndex.x -= 1;
                    else
                    {
                        arrowSelectIndex.x = LETTER_GRID_X - 1;
                        if (arrowSelectIndex.y > 0) arrowSelectIndex.y -= 1;
                    }
                    FinishArrowMove();
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    if (username.text.Length < MAX_CHARACTER_LENGTH)
                    {
                        audioSource.clip = clip[selectionSounds[SelectionSounds.Select]];
                        audioSource.Play();
                        username.text += selectedLetter.text;
                        arrowAnimator.SetTrigger(SELECT_TRIGGER);
                    }
                    else
                    {
                        audioSource.clip = clip[selectionSounds[SelectionSounds.CannotSelect]];
                        audioSource.Play();
                    }
                }

                if (Input.GetKeyDown(KeyCode.X))
                {
                    audioSource.clip = clip[selectionSounds[SelectionSounds.CannotSelect]];
                    audioSource.Play();
                    if (username.text != "")
                    {
                        username.text = username.text.Substring(0, username.text.Length - 1);
                    }
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    nameSelect.SetActive(false);
                    audioSource.clip = clip[selectionSounds[SelectionSounds.Confirm]];
                    audioSource.Play();
                    StartCoroutine(BeginScene());
                }
            }
        }

        private void FinishArrowMove()
        {
            timePassedSinceKey = 0;
            audioSource.clip = clip[selectionSounds[SelectionSounds.Move]];
            audioSource.Play();
        }

        private Text SelectLetter()
        {
            Text selectedLetter = textGrid[(int)arrowSelectIndex.x, (int)arrowSelectIndex.y];
            arrow.transform.position = selectedLetter.transform.position + arrowOffset;
            return selectedLetter;
        }

        private void PopulateLetterGrid()
        {
            int charIndex = 65;

            for (int y = 0; y < LETTER_GRID_Y; y++)
            {
                for (int x = 0; x < LETTER_GRID_X; x++)
                {
                    if (charIndex > 90 && charIndex < 97)
                    {
                        charIndex = 97;
                    }
                    else if (charIndex > 122)
                    {
                        charIndex = 32;
                    }
                    textGrid[x, y] = Instantiate(letterPrefab, letterGrid.transform);
                    textGrid[x, y].text += (char)charIndex;
                    if (charIndex != 32)
                    {
                        charIndex++;
                    }
                }
            }
        }

        IEnumerator BeginScene()
        {
            nameSelect.SetActive(false);
            float alphaFade = 1;
            letterBox.SetActive(true);

            yield return new WaitForSeconds(fadeDelay);
            while (fadePanel.color.a > 0)
            {
                alphaFade -= fadeIncrement;
                fadePanel.color = new Color(0, 0, 0, alphaFade);
                yield return new WaitForSeconds(1f);
            }
            DialogueControlHandler.InitializeEvent(DialogueEventName.Prologue);
            sceneStarted = true;
        }

        IEnumerator EndScene()
        {
            float alphaFade = 0;
            while (fadePanel.color.a < 1)
            {
                alphaFade += fadeIncrement;
                fadePanel.color = new Color(0, 0, 0, alphaFade);
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(fadeDelay);
            SceneManager.LoadScene(introSceneId);
        }
    }
}