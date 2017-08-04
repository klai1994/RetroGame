using UnityEngine;
using UnityEngine.UI;

using Game.CameraUI;
namespace Game.Levels
{
    [RequireComponent(typeof(SelectionCursor))]
    public class NameSelect : MonoBehaviour
    {
        [SerializeField] SelectionCursor selectionCursor;

        public delegate void BroadcastNameSelected();
        public event BroadcastNameSelected NotifyNameSelected;

        [SerializeField] Text userNameSelection;
        const string DEFAULT_NAME = "Mason";
        const int MAX_NAME_LENGTH = 13;

        [SerializeField] Text letterPrefab;
        [SerializeField] GameObject letterGrid;

        const int LETTER_GRID_X = 7;
        const int LETTER_GRID_Y = 8;

        void Start()
        {
            selectionCursor.MenuGrid = new Text[LETTER_GRID_X, LETTER_GRID_Y];
            PopulateLetterGrid();
            selectionCursor.SelectedMenuItem = selectionCursor.MenuGrid[0, 0];
        }

        void Update()
        {
            ProcessCommandInput();
        }

        void ProcessCommandInput()
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
            selectionCursor.PlayAudio(SelectionCursor.CursorSounds.Default);
            userNameSelection.text = DEFAULT_NAME;
        }

        void SelectCharacter()
        {
            if (userNameSelection.text.Length < MAX_NAME_LENGTH)
            {
                selectionCursor.PlayAudio(SelectionCursor.CursorSounds.Select);

                userNameSelection.text += ((Text)selectionCursor.SelectedMenuItem).text;
                selectionCursor.GetAnimator().SetTrigger(SelectionCursor.SELECT_TRIGGER);
            }
            else
            {
                selectionCursor.PlayAudio(SelectionCursor.CursorSounds.CannotSelect);
            }
        }

        void BackSpace()
        {
            selectionCursor.PlayAudio(SelectionCursor.CursorSounds.CannotSelect);

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
                selectionCursor.PlayAudio(SelectionCursor.CursorSounds.CannotSelect);
                userNameSelection.text = "";
                return;
            }

            Game.Actors.PlayerData.PlayerName = selectedName;
            selectionCursor.PlayAudio(SelectionCursor.CursorSounds.Confirm);

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
            selectionCursor.MenuGrid[x, y] = Instantiate(letterPrefab, letterGrid.transform);
            ((Text)selectionCursor.MenuGrid[x, y]).text += charToAdd;
            return ((Text)selectionCursor.MenuGrid[x, y]);
        }

    }
}