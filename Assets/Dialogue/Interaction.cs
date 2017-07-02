using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Dialogue;

namespace Game.Actors.Overworld
{
    public class Interaction : MonoBehaviour
    {

        [SerializeField] DialogueEventName eventName;
        Player player;

        // Use this for initialization
        void Start()
        {
            player = FindObjectOfType<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            if (player.IsInDialogue == true)
            {
                DialogueControlHandler.InitializeEvent(eventName);

                if (DialogueControlHandler.currentEvent == null)
                {
                    player.IsInDialogue = false;
                }
            }
        }
    }
}