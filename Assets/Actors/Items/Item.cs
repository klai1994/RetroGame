using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Actors;
namespace Game.Items
{ 
    public class Item : MonoBehaviour, IRangeable
    {
        //Sprite uiSprite;
        //Sprite worldSprite;
        //bool isDiscardable = false;
        //bool inInventory = false;

        public void Use()
        {
            //
        }

        public void Discard()
        {
            //
        }

        public float GetTargetDistance()
        {
            return Vector2.Distance(PlayerAvatar.GetPlayerInstance().transform.position, transform.position);
        }

    }
}