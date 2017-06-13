using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Entities
{
    [RequireComponent(typeof(RawImage))]
    public class PlayerHealthBar : MonoBehaviour
    {

        RawImage playerHealthBar;
        Player player;

        // Use this for initialization
        void Start()
        {
            playerHealthBar = GetComponent<RawImage>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        // Update is called once per frame
        void Update()
        {
            FillHealthBar();
        }

        private void FillHealthBar()
        {
            float xValue = player.HealthPercentage - 0.5f;
            playerHealthBar.uvRect = new Rect(-xValue, 0f, 1f, 1f);
        }
    }
}
