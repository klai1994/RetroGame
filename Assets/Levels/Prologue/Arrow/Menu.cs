using UnityEngine;
using UnityEngine.UI;

namespace Game.CameraUI {
    [RequireComponent(typeof(AudioSource))]
    public abstract class Menu : MonoBehaviour
    {
        protected float timePassedSinceKey = 0;
        // Delay between items during auto scroll
        protected const float KEY_DELAY = 0.2f;
        // Used for cursor animator
        protected const string SELECT_TRIGGER = "Select";

        // Points at item in the menu that the cursor is currently selecting
        protected Vector2 selectIndex = Vector2.zero;
        protected Vector3 cursorOffset = new Vector3(-30f, -5f, 0);
        // Offset from cursor object to item in menu
        protected Animator animator;
        [SerializeField] protected GameObject cursorHandler;

        [SerializeField] protected GameObject menuUIFrame;
        protected MaskableGraphic[,] menuGrid;
        protected MaskableGraphic selectedMenuItem;

        [SerializeField] protected AudioClip[] soundEffects;
        protected AudioSource audioSource;

        protected enum CursorSounds
        {
            Select = 0,
            CannotSelect = 1,
            Move = 2,
            Confirm = 3,
            Default = 4
        }

        protected void PlaySelectAnim()
        {
            animator.SetTrigger(Menu.SELECT_TRIGGER);
        }

        protected void PlayAudio(CursorSounds soundToPlay)
        {
            audioSource.PlayOneShot(soundEffects[(int)soundToPlay]);
        }

        protected void MoveArrow()
        {
            timePassedSinceKey = 0;
            PlayAudio(Menu.CursorSounds.Move);

            selectedMenuItem = menuGrid[(int)selectIndex.x, (int)selectIndex.y];
            cursorHandler.transform.position = selectedMenuItem.transform.position + cursorOffset;
        }

        protected void InitializeCursor()
        {
            audioSource = GetComponent<AudioSource>();
            animator = GetComponentInChildren<Animator>();
            selectedMenuItem = menuGrid[(int)selectIndex.x, (int)selectIndex.y];
        }

        protected void ProcessCursorInput()
        {
            // Y values are reversed because the grid opens downwards
            if (timePassedSinceKey > Menu.KEY_DELAY)
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

        protected abstract void ProcessCommandInput();

        protected abstract void AddItemToMenu(int x, int y, int index);

    }
}
