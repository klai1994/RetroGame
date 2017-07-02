using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actors
{
    public class DirectionTrigger : MonoBehaviour
    {
        [SerializeField] Player player;
        const float DISABLE_DELAY = 0.05f;

        private void OnEnable()
        {
            StartCoroutine(DisableAfterDelay());
        }

        private IEnumerator DisableAfterDelay()
        {
            yield return new WaitForSeconds(DISABLE_DELAY);
            gameObject.SetActive(false);
        }

        void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<Interaction>())
            {
                player.IsInDialogue = true;
            }
        }
    }
}