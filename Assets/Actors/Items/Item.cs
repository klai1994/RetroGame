using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Actors;
namespace Game.Items
{ 
    public class Item : MonoBehaviour
    {
        //Sprite sprite;
        //bool isDiscardable = false;
        //bool inInventory = false;

        public void Use()
        {
            print("Used item!");
        }

        public void Discard()
        {
            Destroy(gameObject);
        }

    }
}