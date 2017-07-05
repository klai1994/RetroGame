using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Actors;

namespace Game.Dialogue
{
    public class Interaction : MonoBehaviour
    {
        [SerializeField] DialogueEventName[] eventNames;
        [SerializeField] float interactionDistance = 2.5f;
        int interactionIndex = 0;
        PlayerAvatar player;

        Animator animator;
        private const string MOVEMENT_X = "movement_x";
        private const string MOVEMENT_Y = "movement_y";

        // Use this for initialization
        void Start()
        {
            player = FindObjectOfType<PlayerAvatar>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C) && GetDistanceToPlayer() < interactionDistance && !player.InDialogue)
            {
                Interact();
            }
        }

        float GetDistanceToPlayer ()
        {
            return (player.transform.position - transform.position).magnitude;
        }

        public void FacePlayer()
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;

            animator.SetFloat(MOVEMENT_X, direction.x);
            animator.SetFloat(MOVEMENT_Y, direction.y);
        }

        public void Interact()
        {
            player.InDialogue = true;
            player.FaceDirection(transform.position);

            if (animator = GetComponent<Animator>())
            {
                FacePlayer();
            }

            DialogueControlHandler.InitializeEvent(eventNames[interactionIndex]);

            if (interactionIndex < eventNames.Length - 1)
            {
                interactionIndex++;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, interactionDistance);
        }
    }
}
