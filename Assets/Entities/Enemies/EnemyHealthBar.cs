using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Entities
{
    [RequireComponent(typeof(RawImage))]
    public class EnemyHealthBar : MonoBehaviour
    {

        RawImage enemyHealthBar;
        Entity enemy;

        // Use this for initialization
        void Start()
        {
            enemyHealthBar = GetComponent<RawImage>();
            enemy = GetComponentInParent<Entity>();
        }

        // Update is called once per frame
        void Update()
        {
            float xValue = enemy.HealthPercentage - 0.5f;
            enemyHealthBar.uvRect = new Rect(-xValue, 0, 1f, 1f);
        }
    }
}