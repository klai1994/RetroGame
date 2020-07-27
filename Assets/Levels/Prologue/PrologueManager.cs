using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Game.CameraUI.Dialogue;
using Game.Audio;

namespace Game.Levels
{
    public class PrologueManager : MonoBehaviour
    {
        [SerializeField] Image fadePanel = null;
        [SerializeField] GameObject targetExit = null;

        Actors.NPCAI officer;
        DialogueSystem letterbox;
        NameSelect nameselect;
        MusicManager musicManager;

        const int INTRO_SCENE_ID = 1;
        const float FADE_INCREMENT = 0.2f;
        const float FADE_START_DELAY = 5f;
        const float MUSIC_FADE = 0.01f;

        bool sceneStarted = false;
        float alphaFade;

        void Start()
        {
            officer = FindObjectOfType<Actors.NPCAI>();
            letterbox = FindObjectOfType<DialogueSystem>();
            nameselect = FindObjectOfType<NameSelect>();
            nameselect.NotifyNameSelected += StartPrologue;
            musicManager = MusicManager.Instance();
        }

        void Update()
        {
            if (sceneStarted == true && !DialogueSystem.dialogueSystem.EventOccuring)
            {
                // Cues officer to leave scene.
                officer.target = targetExit;
                musicManager.StopMusic(MUSIC_FADE);
                StartCoroutine(FadeOut());

                // Prevents coroutine from being called multiple times
                sceneStarted = !sceneStarted;
            }
        }
        
        void StartPrologue()
        {
            musicManager.PlayMusic(MusicName.LightIntro, MUSIC_FADE);
            StartCoroutine(FadeIn(DialogueEventName.Prologue));
        }

        // Delays fade, then fades in panel and music, finally starting introduction
        public IEnumerator FadeIn(DialogueEventName eventName)
        {
            yield return new WaitForSeconds(FADE_START_DELAY);
            letterbox.gameObject.SetActive(true);
            alphaFade = 1;
            
            while (fadePanel.color.a > 0)
            {
                alphaFade -= FADE_INCREMENT;
                fadePanel.color = new Color(0, 0, 0, alphaFade);
                yield return new WaitForSeconds(1f);

            }

            letterbox.InitiateDialogue(eventName);
            sceneStarted = true;
        }

        // Fades music, then panel, delays scene load then loads scene.
        public IEnumerator FadeOut()
        {
            yield return new WaitForSeconds(FADE_START_DELAY);
            alphaFade = 0;

            while (fadePanel.color.a < 1)
            {
                alphaFade += FADE_INCREMENT;
                fadePanel.color = new Color(0, 0, 0, alphaFade);
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(FADE_START_DELAY);
            nameselect.NotifyNameSelected -= StartPrologue;
            SceneManager.LoadScene(INTRO_SCENE_ID);
        }

    }
}