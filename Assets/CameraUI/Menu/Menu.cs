using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.CameraUI
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class Menu : MonoBehaviour
    {
        [SerializeField] protected GameObject menuUIFrame;
        [SerializeField] protected MaskableGraphic menuItemPrefab;
        protected MaskableGraphic selectedMenuItem;

        protected float timePassedSinceKey = 0;
        // Delay between items during auto scroll
        protected const float KEY_DELAY = 0.2f;
        // Used for cursor animator
        protected const string SELECT_TRIGGER = "Select";

        protected Vector3 cursorOffset = new Vector3(-30f, -5f, 0);
        // Offset from cursor object to item in menu
        protected Animator animator;
        [SerializeField] protected GameObject cursorHandler;
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
            animator.SetTrigger(SELECT_TRIGGER);
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

        protected abstract void MoveArrow();

        protected abstract void ProcessCursorInput();

        protected abstract void ProcessCommandInput();

    }
}