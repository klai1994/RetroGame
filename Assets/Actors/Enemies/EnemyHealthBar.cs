﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Actors
{
    [RequireComponent(typeof(RawImage))]
    public class EnemyHealthBar : MonoBehaviour
    {

        RawImage enemyHealthBar;
        Enemy enemy;

        // Use this for initialization
        void Start()
        {
            enemyHealthBar = GetComponent<RawImage>();
            enemy = GetComponentInParent<Enemy>();
        }

        // Update is called once per frame
        void Update()
        {
            FillHealthBar();
        }

        private void FillHealthBar()
        {
            float xValue = enemy.HealthPercentage - 0.5f;
            enemyHealthBar.uvRect = new Rect(-xValue, 0, 1f, 1f);
        }
    }
}