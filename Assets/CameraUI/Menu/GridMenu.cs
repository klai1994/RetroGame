using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.CameraUI {
    [RequireComponent(typeof(AudioSource))]
    public abstract class GridMenu : Menu
    {
        protected MaskableGraphic[,] menuGrid;
        // Points at item in the menu that the cursor is currently selecting
        protected Vector2 selectIndex = Vector2.zero;

        protected override void MoveArrow()
        {
            timePassedSinceKey = 0;
            PlayAudio(GridMenu.CursorSounds.Move);

            selectedMenuItem = menuGrid[(int)selectIndex.x, (int)selectIndex.y];
            cursorHandler.transform.position = selectedMenuItem.transform.position + cursorOffset;
        }

        protected void InitializeGridMenu(int menuGridX, int menuGridY = 0, UnityAction populateAction = null)
        {
            menuGrid = new MaskableGraphic[menuGridX, menuGridY];
            SetupMenu(populateAction);
            selectedMenuItem = menuGrid[0, 0];
        }

        protected override void ProcessCursorInput()
        {
            // Y values are reversed because the grid opens downwards
            if (timePassedSinceKey > GridMenu.KEY_DELAY)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    if (selectIndex.y > 0)
                    {
                        selectIndex.y -= 1;
                    }
                    else
                    {
                        selectIndex.y = menuGrid.GetLength(1) - 1;
                    }
                    MoveArrow();
                }

                else if (Input.GetKey(KeyCode.S))
                {
                    if (selectIndex.y < menuGrid.GetLength(1) - 1)
                    {
                        selectIndex.y += 1;
                    }
                    else
                    {
                        selectIndex.y = 0;
                    }

                    MoveArrow();
                }

                else if (Input.GetKey(KeyCode.D))
                {
                    if (selectIndex.x < menuGrid.GetLength(0) - 1)
                    {
                        selectIndex.x += 1;
                    }
                    else
                    {
                        selectIndex.x = 0;
                        if (selectIndex.y < menuGrid.GetLength(1) - 1) selectIndex.y += 1;
                    }

                    MoveArrow();
                }

                else if (Input.GetKey(KeyCode.A))
                {
                    if (selectIndex.x > 0)
                    {
                        selectIndex.x -= 1;
                    }
                    else
                    {
                        selectIndex.x = menuGrid.GetLength(0) - 1;
                        if (selectIndex.y > 0) selectIndex.y -= 1;
                    }

                    MoveArrow();
                }
            }
            else
            {
                timePassedSinceKey += Time.deltaTime;
            }
        }

        protected abstract void AddGridMenuItem(int x, int y, int index);

    }
}
