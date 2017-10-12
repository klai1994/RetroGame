using UnityEngine;
using UnityEngine.UI;
using Game.CameraUI;

namespace Game.Levels
{
    public class NameSelect : GridMenu
    {
        public delegate void BroadcastNameSelected();
        public event BroadcastNameSelected NotifyNameSelected;

        [SerializeField] Text userNameSelection;
        int charIndex;

        const string DEFAULT_NAME = "Mason";
        const int MAX_NAME_LENGTH = 13;
        const int GRID_SIZE_X = 7;
        const int GRID_SIZE_Y = 8;

        void Start()
        {
            Init(GRID_SIZE_X, GRID_SIZE_Y, PopulateLetterGrid);
        }

        void Update()
        {
            ProcessKeyInput();
            ProcessCommandInput();
        }

        void ProcessCommandInput()
        {
            // Select default name
            if (Input.GetKeyDown(KeyCode.V))
            {
                PlayAudio(CursorSounds.Default);
                userNameSelection.text = DEFAULT_NAME;
            }

            // Select character
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (userNameSelection.text.Length < MAX_NAME_LENGTH)
                {
                    PlayAudio(CursorSounds.Select);
                    userNameSelection.text += ((Text)selectedMenuItem).text;
                    PlayCursorAnim(SELECT_TRIGGER);
                }
                else
                {
                    PlayAudio(CursorSounds.CannotSelect);
                }
            }

            // Backspace
            if (Input.GetKeyDown(KeyCode.X))
            {
                PlayAudio(CursorSounds.CannotSelect);

                if (userNameSelection.text != "")
                {
                    userNameSelection.text = userNameSelection.text.Substring(0, userNameSelection.text.Length - 1);
                }
            }

            // Complete name selection
            if (Input.GetKeyDown(KeyCode.Return))
            {
                string selectedName = userNameSelection.text.Trim();
                if (selectedName == "")
                {
                    PlayAudio(CursorSounds.CannotSelect);
                    userNameSelection.text = "";
                    return;
                }

                PlayerData.CreateNewPlayer(selectedName);
                PlayAudio(CursorSounds.Confirm);

                NotifyNameSelected();
                gameObject.transform.SetParent(Camera.main.transform);
                Destroy(this);
            }

        }

        protected override void AddGridMenuItem(int x, int y)
        {
            base.AddGridMenuItem(x, y);
            ((Text)menuGrid[x, y]).text += (char)charIndex;
        }

        void PopulateLetterGrid()
        {
            // Refer to ASCII table for char values
            const int SPACE = 32;
            const int UPPER_START = 65;
            const int UPPER_END = 90;
            const int LOWER_START = 97;
            const int LOWER_END = 122;

            charIndex = UPPER_START;

            for (int y = 0; y < GRID_SIZE_Y; y++)
            {
                for (int x = 0; x < GRID_SIZE_X; x++)
                {
                    // Complete last row with spaces
                    if (charIndex > UPPER_END && charIndex < LOWER_START)
                    {
                        AddGridMenuItem(x, y);
                        x++;
                        AddGridMenuItem(x, y);
                        charIndex = LOWER_START;
                        continue;
                    }

                    // Begin printing spaces after lowercase letters complete
                    else if (charIndex > LOWER_END)
                    {
                        charIndex = SPACE;
                    }

                    AddGridMenuItem(x, y);

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