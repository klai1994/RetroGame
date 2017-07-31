using UnityEngine;

public class SelectionArrow : MonoBehaviour {

    Vector3 arrowOffset = new Vector3(-30f, -5f, 0);
    GameObject arrowObject;
    Animator arrowAnimator;
    Vector2 arrowSelectIndex;

    float timePassedSinceKey = 0;
    const float KEY_DELAY = 0.2f;
    const string SELECT_TRIGGER = "Select";

    [SerializeField] AudioClip[] clip;
    AudioSource audioSource;

    enum SelectionSounds
    {
        Select = 0,
        CannotSelect = 1,
        Move = 2,
        Confirm = 3,
        Default = 4
    }

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        arrowAnimator = GetComponentInChildren<Animator>();
        arrowSelectIndex = Vector2.zero;
    }

    // Update is called once per frame
    void Update ()
    {
        DelayArrow();
    }

    void PlayAudio(SelectionSounds soundToPlay)
    {
        audioSource.clip = clip[(int)soundToPlay];
        audioSource.Play();
    }

    void DelayArrow()
    {
        timePassedSinceKey += Time.deltaTime;
    }

}
