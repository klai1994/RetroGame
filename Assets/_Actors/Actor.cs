using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actors
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Actor : MonoBehaviour
    {
        
        public float GetDistance(GameObject target)
        {
            return Vector2.Distance(target.transform.position, transform.position);
        }

    }
}