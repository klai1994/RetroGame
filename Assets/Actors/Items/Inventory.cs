using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Game.CameraUI;
using System;

namespace Game.Items
{
    public class Inventory : ListMenu
    {
        private const int INVENTORY_SLOTS = 10;

        // Use this for initialization
        void Start()
        {
            InitializeListMenu(INVENTORY_SLOTS, ListMenuConfig.Horizontal);
            for (int i = 0; i < menuList.Length; i++)
            {
                AddListItemToMenu(i);
            }
        }

        // Update is called once per frame
        void Update()
        {
            ProcessCursorInput();
            ProcessCommandInput();
        }

        protected override void ProcessCommandInput()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                UseItem();
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                DiscardItem();
            }
        }

        void UseItem()
        {
            Item item = menuList[selectIndexPointer].GetComponent<Item>();
            if (item)
            {
                item.Use();
            }
        }

        void DiscardItem()
        {
            Item item = menuList[selectIndexPointer].GetComponent<Item>();
            if (item)
            {
                item.Discard();
            }
        }

        protected override void AddListItemToMenu(int index)
        {
            if (menuList[index] == null)
            {
                menuList[index] = Instantiate(menuItemPrefab, menuUIFrame.transform);
            }
            else
            {
                index++;
                AddListItemToMenu(index);
            }
        }

    }
}