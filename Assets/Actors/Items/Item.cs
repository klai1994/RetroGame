using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.Actors;
namespace Game.Items
{ 
    public class Item : MonoBehaviour, IRangeable
    {
        Sprite itemIcon;
        Sprite worldIcon;
        bool isDiscardable;
        bool inInventory;

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