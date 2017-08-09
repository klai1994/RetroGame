using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.CameraUI;
using System;

namespace Game.Items
{
    public class Inventory : GridMenu
    {

        //private const int INVENTORY_SLOTS = 10;
        //private Item[] inventoryItems = new Item[INVENTORY_SLOTS];

        // Use this for initialization
        void Start()
        {
     
        }

        // Update is called once per frame
        void Update()
        {
            ProcessCommandInput();
        }

        protected override void ProcessCommandInput()
        {
            //
        }

        protected override void AddItemToMenu(int x, int y, int index)
        {
            //
        }
    }
}