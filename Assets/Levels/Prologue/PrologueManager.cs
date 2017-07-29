using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Game.Dialogue;
using Game.Audio;

namespace Game.Levels
{
    public class PrologueManager : MonoBehaviour
    {
        [SerializeField] Image fadePanel;
        [SerializeField] LetterboxManager letterBoxManager;
        NameSelect nameselect;

        const int INTRO_SCENE_ID = 1;
        const float FADE_INCREMENT = 0.2f;
        const float FADE_START_DELAY = 5f;
        const float MUSIC_FADE = 0.1f;

        bool sceneStarted = false;
        float alphaFade;

        void Start()
        {
            nameselect = FindObjectOfType<NameSelect>();
            nameselect.broadcastNameSelect += StartPrologue;
        }

        void Update()
        {
            if (sceneStarted == true && DialogueControlHandler.currentEvent == null)
            {
                StartCoroutine(FadeOut());

                // Prevents coroutine from being called multiple times
                sceneStarted = !sceneStarted;
            }
        }
        
        private void StartPrologue()
        {
            StartCoroutine(FadeIn());
        }

        // Delays fade, then fades in panel and music, finally starting introduction
        public IEnumerator FadeIn()
        {
            yield return new WaitForSeconds(FADE_START_DELAY);
            letterBoxManager.gameObject.SetActive(true);
            alphaFade = 1;

            while (fadePanel.color.a > 0)
            {
                alphaFade -= FADE_INCREMENT;
                fadePanel.color = new Color(0, 0, 0, alphaFade);
                yield return new WaitForSeconds(1f);

            }

            DialogueControlHandler.InitializeEvent(DialogueEventName.Prologue);
            sceneStarted = true;
        }

        // Fades music, then panel, delays scene load then loads scene
        public IEnumerator FadeOut()
        {
            alphaFade = 0;

            while (fadePanel.color.a < 1)
            {
                alphaFade += FADE_INCREMENT;
                fadePanel.color = new Color(0, 0, 0, alphaFade);
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(FADE_START_DELAY);
            SceneManager.LoadScene(INTRO_SCENE_ID);
        }

    }
}