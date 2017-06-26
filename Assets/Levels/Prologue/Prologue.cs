using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Game.Dialogue;
using System;
using UnityEngine.SceneManagement;

public class Prologue : MonoBehaviour {

    [SerializeField] Image fadePanel;
    [SerializeField] float fadeIncrement;
    [SerializeField] float loadDelay;
    [SerializeField] int introScene;
    bool fadeStarted;

    // Use this for initialization
    void Start ()
    {
        fadeStarted = false;
        DialogueHandler.InitializeEvent(DialogueEventName.Prologue);
    }

    void Update()
    {
        if (DialogueHandler.currentEvent == null && fadeStarted == false)
        {
            fadeStarted = true;
            StartCoroutine(EndScene());
        }
    }

    IEnumerator EndScene()
    {
        float alphaFade = 0;
        while (fadePanel.color.a < 1)
        {
            alphaFade += fadeIncrement;
            fadePanel.color = new Color(0, 0, 0, alphaFade);
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(introScene);
    }
}
