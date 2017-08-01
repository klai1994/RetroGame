using UnityEngine;
using UnityEngine.UI;

using Game.CameraUI;
namespace Game.Levels
{
    [RequireComponent(typeof(SelectionCursor))]
    public class NameSelect : MonoBehaviour
    {
        [SerializeField] SelectionCursor selectionArrow;

        public delegate void BroadcastNameSelected();
        public event BroadcastNameSelected NotifyNameSelected;

        [SerializeField] Text userNameSelection;
        const string DEFAULT_NAME = "Mason";
        const int MAX_NAME_LENGTH = 13;

        Text selectedLetter;
        [SerializeField] Text letterPrefab;
        [SerializeField] GameObject letterGrid;

        const int LETTER_GRID_X = 7;
        const int LETTER_GRID_Y = 8;
        Text[,] textGrid = new Text[LETTER_GRID_X, LETTER_GRID_Y];

        void Start()
        {
            PopulateLetterGrid();
            selectedLetter = textGrid[0, 0];
        }

        void Update()
        {
            ProcessArrowInput();
            ProcessCommandInput();
        }

        private void ProcessCommandInput()
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

        private void ProcessArrowInput()
        {
            // Y values are reversed because the grid opens downwards
            if (selectionArrow.TimePassedSinceKey > SelectionCursor.KEY_DELAY)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    if (selectionArrow.SelectIndex.y > 0)
                    {
                        selectionArrow.SelectIndex.y -= 1;
                    }
                    else
                    {
                        selectionArrow.SelectIndex.y = LETTER_GRID_Y - 1;
                    }

                    MoveArrow();
                }

                else if (Input.GetKey(KeyCode.S))
                {
                    if (selectionArrow.SelectIndex.y < LETTER_GRID_Y - 1)
                    {
                        selectionArrow.SelectIndex.y += 1;
                    }
                    else
                    {
                        selectionArrow.SelectIndex.y = 0;
                    }

                    MoveArrow();
                }

                else if (Input.GetKey(KeyCode.D))
                {
                    if (selectionArrow.SelectIndex.x < LETTER_GRID_X - 1)
                    {
                        selectionArrow.SelectIndex.x += 1;
                    }
                    else
                    {
                        selectionArrow.SelectIndex.x = 0;
                        if (selectionArrow.SelectIndex.y < LETTER_GRID_Y - 1) selectionArrow.SelectIndex.y += 1;
                    }

                    MoveArrow();
                }

                else if (Input.GetKey(KeyCode.A))
                {
                    if (selectionArrow.SelectIndex.x > 0)
                    {
                        selectionArrow.SelectIndex.x -= 1;
                    }
                    else
                    {
                        selectionArrow.SelectIndex.x = LETTER_GRID_X - 1;
                        if (selectionArrow.SelectIndex.y > 0) selectionArrow.SelectIndex.y -= 1;
                    }

                    MoveArrow();
                }
            }
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

        private void SelectDefaultName()
        {
            selectionArrow.PlayAudio(SelectionCursor.CursorSounds.Default);
            userNameSelection.text = DEFAULT_NAME;
        }

        void SelectCharacter()
        {
            if (userNameSelection.text.Length < MAX_NAME_LENGTH)
            {
                selectionArrow.PlayAudio(SelectionCursor.CursorSounds.Select);

                userNameSelection.text += selectedLetter.text;
                selectionArrow.GetAnimator().SetTrigger(SelectionCursor.SELECT_TRIGGER);
            }
            else
            {
                selectionArrow.PlayAudio(SelectionCursor.CursorSounds.CannotSelect);
            }
        }

        void BackSpace()
        {
            selectionArrow.PlayAudio(SelectionCursor.CursorSounds.CannotSelect);

            if (userNameSelection.text != "")
            {
                userNameSelection.text = userNameSelection.text.Substring(0, userNameSelection.text.Length - 1);
            }
        }

        void MoveArrow()
        {
            selectionArrow.TimePassedSinceKey = 0;
            selectionArrow.PlayAudio(SelectionCursor.CursorSounds.Move);

            Text highlightedLetter = textGrid[(int)selectionArrow.SelectIndex.x, 
                (int)selectionArrow.SelectIndex.y];
            selectionArrow.transform.position = highlightedLetter.transform.position + selectionArrow.GetOffset();
            selectedLetter = highlightedLetter;
        }

        void ConfirmNameSelection()
        {
            string selectedName = userNameSelection.text.Trim();
            if (selectedName == "")
            {
                selectionArrow.PlayAudio(SelectionCursor.CursorSounds.CannotSelect);
                userNameSelection.text = "";
                return;
            }

            Game.Actors.PlayerData.PlayerName = selectedName;
            selectionArrow.PlayAudio(SelectionCursor.CursorSounds.Confirm);

            NotifyNameSelected();
            gameObject.transform.SetParent(Camera.main.transform);
            Destroy(this);
        }

    }
}