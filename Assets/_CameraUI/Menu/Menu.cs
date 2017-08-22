using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.CameraUI
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class Menu : MonoBehaviour
    {
        [SerializeField] protected LayoutGroup menuUIFrame;
        [SerializeField] protected MaskableGraphic defaultMenuItemPrefab;
        protected MaskableGraphic selectedMenuItem;

        protected float timePassedSinceKey = 0;
        // Delay between items during auto scroll
        protected const float KEY_DELAY = 0.2f;
        // Used for cursor animator
        protected const string SELECT_TRIGGER = "Select";
        protected const string CANNOT_SELECT_TRIGGER = "Cannot_Select";

        // Offset from cursor object to item in menu
        [SerializeField] Vector3 cursorOffset = new Vector3(-30f, -5f, 0);
        [SerializeField] protected GameObject cursorHandler;
        protected Animator animator;

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

        protected void PlayCursorAnim(string trigger)
        {
            animator.SetTrigger(trigger );
        }

        protected void PlayAudio(CursorSounds soundToPlay)
        {
            audioSource.PlayOneShot(soundEffects[(int)soundToPlay]);
        }

        protected void SetupMenu(UnityAction populateAction)
        {
            if (populateAction != null)
            {
                populateAction();
            }
            animator = GetComponentInChildren<Animator>();
            audioSource = GetComponent<AudioSource>();
        }

        protected void MoveArrow()
        {
            timePassedSinceKey = 0;
            PlayAudio(GridMenu.CursorSounds.Move);
            SetSelectedItem();
            cursorHandler.transform.position = selectedMenuItem.transform.position + cursorOffset;
        }

        protected void ProcessCursorInput()
        {
            if (timePassedSinceKey > GridMenu.KEY_DELAY)
            {
                SetSelection();
            }
            timePassedSinceKey += Time.deltaTime;
        }

        protected abstract void SetSelectedItem();

        protected abstract void SetSelection();

        protected abstract void ProcessCommandInput();

    }
}