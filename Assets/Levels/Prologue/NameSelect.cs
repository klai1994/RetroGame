using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Levels.Prologue
{
    public class NameSelect : MonoBehaviour
    {
        [SerializeField] Text username;
        PrologueManager prologueManager;

        [SerializeField] Text letterPrefab;
        [SerializeField] GameObject letterGrid;
        Text selectedLetter;
        Text[,] textGrid;

        [SerializeField] GameObject arrowObject;
        [SerializeField] Vector3 arrowOffset;
        [SerializeField] Animator arrowAnimator;
        Vector2 arrowSelectIndex;

        float timePassedSinceKey = 0;
        const float KEY_DELAY = 0.2f;

        const int LETTER_GRID_X = 7;
        const int LETTER_GRID_Y = 8;
        const int MAX_NAME_LENGTH = 13;
        const string SELECT_TRIGGER = "Select";

        [SerializeField] AudioClip[] clip;
        AudioSource audioSource;

        enum SelectionSounds
        {
            Select,
            CannotSelect,
            Move,
            Confirm
        }
        Dictionary<SelectionSounds, int> selectionSounds;

        // Use this for initialization
        void Start()
        {
            prologueManager = FindObjectOfType<PrologueManager>();

            textGrid = new Text[LETTER_GRID_X, LETTER_GRID_Y];
            PopulateLetterGrid();
            selectedLetter = textGrid[0, 0];

            InitializeSounds();
            audioSource = GetComponent<AudioSource>();
            arrowSelectIndex = Vector2.zero;
        }

        // Update is called once per frame
        void Update()
        {
            DelayArrow();

            #region Move Arrow

            // Y values are reversed because the grid opens downwards
            if (timePassedSinceKey > KEY_DELAY)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    if (arrowSelectIndex.y > 0)
                    {
                        arrowSelectIndex.y -= 1;
                    }
                    else
                    {
                        arrowSelectIndex.y = LETTER_GRID_Y - 1;
                    }

                    MoveArrow();
                }

                else if (Input.GetKey(KeyCode.S))
                {
                    if (arrowSelectIndex.y < LETTER_GRID_Y - 1)
                    {
                        arrowSelectIndex.y += 1;
                    }
                    else
                    {
                        arrowSelectIndex.y = 0;
                    }

                    MoveArrow();
                }

                else if (Input.GetKey(KeyCode.D))
                {
                    if (arrowSelectIndex.x < LETTER_GRID_X - 1)
                    {
                        arrowSelectIndex.x += 1;
                    }
                    else
                    {
                        arrowSelectIndex.x = 0;
                        if (arrowSelectIndex.y < LETTER_GRID_Y - 1) arrowSelectIndex.y += 1;
                    }

                    MoveArrow();
                }

                else if (Input.GetKey(KeyCode.A))
                {
                    if (arrowSelectIndex.x > 0)
                    {
                        arrowSelectIndex.x -= 1;
                    }
                    else
                    {
                        arrowSelectIndex.x = LETTER_GRID_X - 1;
                        if (arrowSelectIndex.y > 0) arrowSelectIndex.y -= 1;
                    }

                    MoveArrow();
                }
            }

            #endregion

            #region Commands

            if (Input.GetKeyDown(KeyCode.C))
            {
                SelectCharacter();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                BackSpace();
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                ConfirmNameSelection();
            }

            #endregion

        }

        void PopulateLetterGrid()
        {
            // Refer to ASCII table for char values
            const int SPACE = 32;
            const int UPPER_START = 65;
            const int UPPER_END = 90;
            const int LOWER_START = 97;
            const int LOWER_END = 122;

            int charIndex = UPPER_START;

            for (int y = 0; y < LETTER_GRID_Y; y++)
            {
                for (int x = 0; x < LETTER_GRID_X; x++)
                {
                    // If uppercase letters filled in, complete last row with spaces
                    if (charIndex > UPPER_END && charIndex < LOWER_START)
                    {
                        AddCharToGrid(x, y, SPACE);
                        x++;
                        AddCharToGrid(x, y, SPACE);
                        charIndex = LOWER_START;
                        continue;
                    }

                    // Begin printing spaces after lowercase letters complete
                    else if (charIndex > LOWER_END)
                    {
                        charIndex = SPACE;
                    }

                    AddCharToGrid(x, y, charIndex);

                    // Stop incrementing char index after lowercase letters completed
                    if (charIndex != SPACE)
                    {
                        charIndex++;
                    }
                }
            }
        }

        Text AddCharToGrid(int x, int y, int charIndex)
        {
            char charToAdd = (char)charIndex;
            textGrid[x, y] = Instantiate(letterPrefab, letterGrid.transform);
            textGrid[x, y].text += charToAdd;
            return textGrid[x, y];
        }

        void SelectCharacter()
        {
            if (username.text.Length < MAX_NAME_LENGTH)
            {
                PlayAudio(SelectionSounds.Select);

                username.text += selectedLetter.text;
                arrowAnimator.SetTrigger(SELECT_TRIGGER);
            }
            else
            {
                PlayAudio(SelectionSounds.CannotSelect);
            }
        }

        void BackSpace()
        {
            PlayAudio(SelectionSounds.CannotSelect);
            if (username.text != "")
            {
                username.text = username.text.Substring(0, username.text.Length - 1);
            }
        }

        void MoveArrow()
        {
            timePassedSinceKey = 0;
            PlayAudio(SelectionSounds.Move);

            Text highlightedLetter = textGrid[(int)arrowSelectIndex.x, (int)arrowSelectIndex.y];
            arrowObject.transform.position = highlightedLetter.transform.position + arrowOffset;
            selectedLetter = highlightedLetter;
        }

        void ConfirmNameSelection()
        {
            string selectedName = username.text.Trim();
            if (selectedName == "")
            {
                PlayAudio(SelectionSounds.CannotSelect);
                username.text = "";
                return;
            }

            Game.Entities.Player.playerName = selectedName;
            PlayAudio(SelectionSounds.Confirm);

            prologueManager.StartFade(false);
            gameObject.transform.SetParent(prologueManager.transform);
            Destroy(this);
        }

        void DelayArrow()
        {
            timePassedSinceKey += Time.deltaTime;
        }

        void InitializeSounds()
        {
            selectionSounds = new Dictionary<SelectionSounds, int> {
                { SelectionSounds.Select, 0 },
                { SelectionSounds.CannotSelect, 1 },
                { SelectionSounds.Move, 2 },
                { SelectionSounds.Confirm, 3 }
            };
        }

        void PlayAudio(SelectionSounds soundToPlay)
        {
            audioSource.clip = clip[selectionSounds[soundToPlay]];
            audioSource.Play();
        }

    }
}