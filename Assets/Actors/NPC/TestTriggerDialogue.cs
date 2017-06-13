using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Dialogue;
public class TestTriggerDialogue : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        DialogueHandler.InitializeEvent((int)DialogueEventName.TestEvent1);
    }
}
