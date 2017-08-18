using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.CameraUI
{
    public abstract class GridMenu : Menu
    {
        protected MaskableGraphic[,] menuGrid;
        protected Vector2 selectIndexPointer = Vector2.zero;

        protected abstract void AddGridMenuItem(int x, int y, int index);

        protected override void SetSelectedItem()
        {
            selectedMenuItem = menuGrid[(int)selectIndexPointer.x, (int)selectIndexPointer.y];
        }

        protected override void SetSelection()
        {
            // Y values are reversed because the grid opens downwards
            if (Input.GetKey(KeyCode.W))
            {
                if (selectIndexPointer.y > 0)
                {
                    selectIndexPointer.y -= 1;
                }
                else
                {
                    selectIndexPointer.y = menuGrid.GetLength(1) - 1;
                }
                MoveArrow();
            }

            else if (Input.GetKey(KeyCode.S))
            {
                if (selectIndexPointer.y < menuGrid.GetLength(1) - 1)
                {
                    selectIndexPointer.y += 1;
                }
                else
                {
                    selectIndexPointer.y = 0;
                }

                MoveArrow();
            }

            else if (Input.GetKey(KeyCode.D))
            {
                if (selectIndexPointer.x < menuGrid.GetLength(0) - 1)
                {
                    selectIndexPointer.x += 1;
                }
                else
                {
                    selectIndexPointer.x = 0;
                    if (selectIndexPointer.y < menuGrid.GetLength(1) - 1)
                    {
                        selectIndexPointer.y += 1;
                    }
                }

                MoveArrow();
            }

            else if (Input.GetKey(KeyCode.A))
            {
                if (selectIndexPointer.x > 0)
                {
                    selectIndexPointer.x -= 1;
                }
                else
                {
                    selectIndexPointer.x = menuGrid.GetLength(0) - 1;
                    if (selectIndexPointer.y > 0)
                    {
                        selectIndexPointer.y -= 1;
                    }
                }

                MoveArrow();
            }
        }

        protected void InitializeGridMenu(int sizeX, int sizeY, UnityAction populateAction = null)
        {
            menuGrid = new MaskableGraphic[sizeX, sizeY];
            SetupMenu(populateAction);
            selectedMenuItem = menuGrid[0, 0];
        }
    }
}
