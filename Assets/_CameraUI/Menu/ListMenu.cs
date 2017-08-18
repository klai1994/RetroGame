using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.CameraUI
{
    public abstract class ListMenu : Menu
    {
        protected MaskableGraphic[] menuList;
        protected int selectIndexPointer = 0;

        public enum ListMenuConfig
        {
            Horizontal, Vertical
        }
        ListMenuConfig config;

        protected abstract void AddListItemToMenu(int index);

        protected void InitializeListMenu(int size, ListMenuConfig config, UnityAction populateAction = null)
        {
            this.config = config;
            menuList = new MaskableGraphic[size];
            SetupMenu(populateAction);
            selectedMenuItem = menuList[0];
        }

        protected override void SetSelectedItem()
        {
            selectedMenuItem = menuList[selectIndexPointer];
        }

        protected override void SetSelection()
        {
            if (config == ListMenuConfig.Horizontal)
            {
                GetHorizontalInput();
            }
            else if (config == ListMenuConfig.Vertical)
            {
                GetVerticalInput();
            }
        }

        void GetHorizontalInput()
        {
            if (Input.GetKey(KeyCode.A))
            {
                IncrementPointerIndex();

            }
            else if (Input.GetKey(KeyCode.D))
            {
            DecrementPointerIndex();

            }
        }

        void GetVerticalInput()
        {
            if (Input.GetKey(KeyCode.W))
            {
                IncrementPointerIndex();
            }
            else if (Input.GetKey(KeyCode.S))
            {
                DecrementPointerIndex();
            }
        }

        void IncrementPointerIndex()
        {
            if (selectIndexPointer < menuList.Length - 1)
            {
                selectIndexPointer += 1;
            }
            else
            {
                selectIndexPointer = menuList.Length - 1;
            }
            MoveArrow();
        }

        void DecrementPointerIndex()
        {
            if (selectIndexPointer > 0)
            {
                selectIndexPointer -= 1;
            }
            else
            {
                selectIndexPointer = 0;
            }
            MoveArrow();
        }

    }
}