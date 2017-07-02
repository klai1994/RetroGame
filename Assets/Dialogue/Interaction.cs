using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Actors;
using Game.Dialogue;
public class Interaction : MonoBehaviour {

    [SerializeField] DialogueEventName eventName;
    const float DISTANCE_THRESHOLD = 2f;
    Player player;

    // Use this for initialization
    void Start () {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update () {

		if (player.IsInDialogue == true && GetDistanceToPlayer() < DISTANCE_THRESHOLD)
        {
            DialogueControlHandler.InitializeEvent(eventName);
        }
        if (player.IsInDialogue == true && DialogueControlHandler.currentEvent == null)
        {
            player.IsInDialogue = false;
        }
    }

    float GetDistanceToPlayer()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        return distance;
    }
}
