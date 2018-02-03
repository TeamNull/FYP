using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public const int itemSlotsNum = 24; //total solts
    public List<Item> itemList = new List<Item>(); //testing use
    public Image[] itemImages = new Image[itemSlotsNum]; //show in ui

    int itemInInventory;

    public void AddItem(Item item, int unit)
    {
        if (itemInInventory == itemSlotsNum) return; //Check bag space

        for (int i = 0; i < itemInInventory; i++) // loop the inventory
        {
            if (itemList[i].id == item.id)  // find the item in inventory
            {
                itemList[i].UpdateUnit(unit);
                return;
            }
        }

        itemList.Add(item);
        itemImages[itemInInventory].sprite = item.sprite;
        itemImages[itemInInventory].enabled = true;
        itemList[itemInInventory].unit++;
        itemInInventory++;
    }

    public void RemoveItem(int inventoryId)
    {
        if (itemInInventory == 0) return;

        itemList[inventoryId].unit--;
        if (itemList[inventoryId].unit == 0) {
            itemInInventory--;
            itemList.RemoveAt(inventoryId);
            itemImages[inventoryId].sprite = null;
            itemImages[inventoryId].enabled = false;
        }
    }

    public bool Cotainitem(int itemid)
    {
        for (int iteminventoryid = 0; iteminventoryid < itemSlotsNum; iteminventoryid++) // loop the inventory
        {
            if (itemList[iteminventoryid].id == itemid)
                return true;
        }

        return false;
    }
}


