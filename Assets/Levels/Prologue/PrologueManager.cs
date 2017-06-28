using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.Dialogue;
using System;
using UnityEngine.SceneManagement;

namespace Game.Levels.Prologue
{
    public class PrologueManager : MonoBehaviour
    {
        [SerializeField] Image fadePanel;
        [SerializeField] LetterboxManager letterBoxManager;

        const int INTRO_SCENE_ID = 1;
        const float FADE_INCREMENT = 0.2f;
        const float FADE_START_DELAY = 5f;
        bool sceneStarted = false;

        void Update()
        {
            if (sceneStarted == true && DialogueControlHandler.currentEvent == null)
            {
                StartCoroutine(FadeScreen(sceneStarted));
                
                // Prevents coroutine from being called multiple times
                sceneStarted = !sceneStarted;
            }
        }

        // Prevents calling object from having to be responsible for coroutine
        public void StartFade(bool prologueCompleted)
        {
            StartCoroutine(FadeScreen(prologueCompleted));
        }

        public IEnumerator FadeScreen(bool prologueCompleted)
        {
            float alphaFade;

            if (prologueCompleted)
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

            else
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
        }
    }
}