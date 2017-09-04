using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Items
{
    public class Item : MonoBehaviour
    {
        public enum ItemType
        {
            // TODO list items to be swapped out for empty slots
        }

        public bool Discardable;

        public void Use()
        {
            print("Used item!");
        }
    }
}