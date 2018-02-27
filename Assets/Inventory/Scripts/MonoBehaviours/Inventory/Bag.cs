using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bag : MonoBehaviour
{
    public const int itemSlotsNum = 24; //total solts
    public List<Item> itemList = new List<Item>(); //testing use
    public Image[] itemImages = new Image[itemSlotsNum]; //show in ui

    int itemInBag;

    private void Awake()
    {
        StaticVarAndFunction.bag = this;
    }

    public void AddItem(Item item, int unit)
    {
        if (itemInBag == itemSlotsNum) return; //Check bag space

        for (int i = 0; i < itemInBag; i++) // loop the inventory
        {
            if (itemList[i].id == item.id)  // find the item in inventory
            {
                itemList[i].UpdateUnit(unit);
                return;
            }
        }

        itemList.Add(item);
        itemImages[itemInBag].sprite = item.sprite;
        itemImages[itemInBag].enabled = true;
        itemList[itemInBag].unit++;
        itemInBag++;
    }

    public void RemoveItem(int inventoryId)
    {
        if (itemInBag == 0) return;

        itemList[inventoryId].unit--;
        if (itemList[inventoryId].unit == 0) {
            itemInBag--;
            itemList.RemoveAt(inventoryId);
            itemImages[inventoryId].sprite = null;
            itemImages[inventoryId].enabled = false;
        }
    }

    public bool Cotainitem(int itemid)
    {
        for (int itemBagid = 0; itemBagid < itemSlotsNum; itemBagid++) // loop the inventory
        {
            if (itemList[itemBagid].id == itemid)
                return true;
        }

        return false;
    }
}


