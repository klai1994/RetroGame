using Game.CameraUI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Items
{
    // Note that the defaultMenuItemPrefab is reserved as an empty slot on the inventory menu.
    public class Inventory : ListMenu
    {
        private const int INVENTORY_SLOTS = 10;
        Item selectedItem;

        // Use this for initialization
        void Start()
        {
            InitializeListMenu(INVENTORY_SLOTS, ListMenuConfig.Horizontal);
            for (int i = 0; i < menuList.Length; i++)
            {
                AddListItemToMenu(defaultMenuItemPrefab, i);
            }
            gameObject.SetActive(false);
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
                selectedItem = menuList[selectIndexPointer].GetComponent<Item>();
                selectedItem.Use();

                PlayCursorAnim(SELECT_TRIGGER);
                PlayAudio(CursorSounds.Select);
            }

            else if (Input.GetKeyDown(KeyCode.X))
            {
                selectedItem = menuList[selectIndexPointer].GetComponent<Item>();

                if (selectedItem.Discardable)
                {
                    PlayCursorAnim(CANNOT_SELECT_TRIGGER);
                    PlayAudio(CursorSounds.CannotSelect);
                    AddListItemToMenu(defaultMenuItemPrefab, selectIndexPointer);
                }
        
            }
        }

        protected override void AddListItemToMenu(MaskableGraphic itemToAdd, int index)
        {
            menuList[index] = Instantiate(itemToAdd, menuUIFrame.transform);
        }

    }
}