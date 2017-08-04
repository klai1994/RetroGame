using UnityEngine;
using UnityEngine.UI;

using Game.CameraUI;
namespace Game.Levels
{
    public class NameSelect : Menu
    {
        public delegate void BroadcastNameSelected();
        public event BroadcastNameSelected NotifyNameSelected;

        [SerializeField] Text userNameSelection;
        const string DEFAULT_NAME = "Mason";
        const int MAX_NAME_LENGTH = 13;

        [SerializeField] Text letterPrefab;
        const int LETTER_GRID_X = 7;
        const int LETTER_GRID_Y = 8;

        void Start()
        {
            menuGrid = new Text[LETTER_GRID_X, LETTER_GRID_Y];
            PopulateLetterGrid();
            InitializeCursor();
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

        void SelectDefaultName()
        {
            PlayAudio(Menu.CursorSounds.Default);
            userNameSelection.text = DEFAULT_NAME;
        }

        void SelectCharacter()
        {
            if (userNameSelection.text.Length < MAX_NAME_LENGTH)
            {
                PlayAudio(Menu.CursorSounds.Select);
                userNameSelection.text += ((Text)selectedMenuItem).text;
                animator.SetTrigger(SELECT_TRIGGER);
            }
            else
            {
                PlayAudio(Menu.CursorSounds.CannotSelect);
            }
        }

        void BackSpace()
        {
            PlayAudio(Menu.CursorSounds.CannotSelect);

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
                PlayAudio(Menu.CursorSounds.CannotSelect);
                userNameSelection.text = "";
                return;
            }

            Actors.PlayerData.PlayerName = selectedName;
            PlayAudio(Menu.CursorSounds.Confirm);

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
                        AddItemToMenu(x, y, SPACE, letterPrefab);
                        x++;
                        AddItemToMenu(x, y, SPACE, letterPrefab);
                        charIndex = LOWER_START;
                        continue;
                    }

                    // Begin printing spaces after lowercase letters complete
                    else if (charIndex > LOWER_END)
                    {
                        charIndex = SPACE;
                    }

                    AddItemToMenu(x, y, charIndex, letterPrefab);

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