using UnityEngine;
using UnityEngine.UI;

using Game.CameraUI;
using System;

namespace Game.Levels
{
    public class NameSelect : GridMenu
    {
        public delegate void BroadcastNameSelected();
        public event BroadcastNameSelected NotifyNameSelected;

        [SerializeField] Text userNameSelection;
        const string DEFAULT_NAME = "Mason";
        const int MAX_NAME_LENGTH = 13;

        const int LETTER_GRID_X = 7;
        const int LETTER_GRID_Y = 8;

        void Start()
        {
            InitializeGridMenu(LETTER_GRID_X, LETTER_GRID_Y, PopulateLetterGrid);
        }

        void Update()
        {
            ProcessCursorInput();
            ProcessCommandInput();
        }

        protected override void ProcessCommandInput()
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                SelectDefaultName();
            }

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
        }

        protected override void AddItemToMenu(int x, int y, int index)
        {
            // In this case index is used as a char
            menuGrid[x, y] = Instantiate(menuItemPrefab, menuUIFrame.transform);
            ((Text)menuGrid[x, y]).text += (char)index;
        }

        void SelectDefaultName()
        {
            PlayAudio(GridMenu.CursorSounds.Default);
            userNameSelection.text = DEFAULT_NAME;
        }

        void SelectCharacter()
        {
            if (userNameSelection.text.Length < MAX_NAME_LENGTH)
            {
                PlayAudio(GridMenu.CursorSounds.Select);
                userNameSelection.text += ((Text)selectedMenuItem).text;
                animator.SetTrigger(SELECT_TRIGGER);
            }
            else
            {
                PlayAudio(GridMenu.CursorSounds.CannotSelect);
            }
        }

        void BackSpace()
        {
            PlayAudio(GridMenu.CursorSounds.CannotSelect);

            if (userNameSelection.text != "")
            {
                userNameSelection.text = userNameSelection.text.Substring(0, userNameSelection.text.Length - 1);
            }
        }

        void ConfirmNameSelection()
        {
            string selectedName = userNameSelection.text.Trim();
            if (selectedName == "")
            {
                PlayAudio(GridMenu.CursorSounds.CannotSelect);
                userNameSelection.text = "";
                return;
            }

            Actors.PlayerData.PlayerName = selectedName;
            PlayAudio(GridMenu.CursorSounds.Confirm);

            NotifyNameSelected();
            gameObject.transform.SetParent(Camera.main.transform);
            Destroy(this);
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
                        AddItemToMenu(x, y, SPACE);
                        x++;
                        AddItemToMenu(x, y, SPACE);
                        charIndex = LOWER_START;
                        continue;
                    }

                    // Begin printing spaces after lowercase letters complete
                    else if (charIndex > LOWER_END)
                    {
                        charIndex = SPACE;
                    }

                    AddItemToMenu(x, y, charIndex);

                    // Stop incrementing char index after lowercase letters completed
                    if (charIndex != SPACE)
                    {
                        charIndex++;
                    }
                }
            }
        }

    }
}