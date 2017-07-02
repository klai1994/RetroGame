using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Actors.Overworld
{
    public abstract class Actor : MonoBehaviour
    {
        [SerializeField] protected float movementSpeed = 1f;

        public enum Directions { Up, Down, Left, Right };
        protected Directions direction;
        protected Dictionary<Directions, Vector2> directionMagnitudes;
        protected const float DIRECTION_THRESHOLD = 0.2f;

        protected Rigidbody2D rbody;
        protected Animator animator;

        public abstract void CheckDirectionFacing();

        public void OnEnable()
        {
            directionMagnitudes = new Dictionary<Directions, Vector2>
            {
                { Directions.Left, Vector2.left },
                { Directions.Right, Vector2.right },
                { Directions.Down, Vector2.down },
                { Directions.Up, Vector2.up }
            };

            rbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

    }
}