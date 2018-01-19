using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour {

    public const int numItemSlots = 4;

    public Image[] itemImages = new Image[numItemSlots];
    public Item[] items = new Item[numItemSlots];

    public void AddItem (Item itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if(items[i] == null)
            {
                items[i] = itemToAdd;
                itemImages[i].sprite = itemToAdd.sprite;
                itemImages[i].enabled = true; // Disable because do not want to show background image
                return;
            }
        }
    }

    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == itemToRemove)
            {
                items[i] = null;
                itemImages[i].sprite = null;
                itemImages[i].enabled = false; // Disable because do not want to show background image
                return;
            }
        }
    }
}
