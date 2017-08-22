using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Items
{
    public class Item : MonoBehaviour
    {
        public bool Discardable;

        public void Use()
        {
            print("Used item!");
        }
    }
}