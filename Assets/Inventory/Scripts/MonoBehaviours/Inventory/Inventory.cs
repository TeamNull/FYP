using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour {

    const int itemSlotsNum = 24; //total solts
    List<Item> itemList = new List<Item>(); //testing use
    List<Image> itemImages = new List<Image>(); //show in ui

    int itemInInventory;

    private void Awake()
    {
        StaticVarAndFunction.inventory = this;
    }

    private void Start()
    {
        List<Transform> childs = new List<Transform>();
        // GetComponentsInChildren does not guarantee the order so do the sorting by ourselves
        for (int i = 0; i < itemSlotsNum; i++)
        {
            childs.Add(transform.GetChild(i));
        }
        foreach (Transform child in childs)
        {
            itemImages.Add(child.GetChild(0).GetComponent<Image>());
        }
        itemImages.OrderBy(x => x.name);
    }

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
        if (itemList[inventoryId].unit == 0)
        {
            itemInInventory--;
            itemList.RemoveAt(inventoryId);
            itemImages[inventoryId].sprite = null;
            itemImages[inventoryId].enabled = false;
        }
    }

    public bool Cotainitem(int itemid)
    {
        for (int itemInventoryId = 0; itemInventoryId < itemSlotsNum; itemInventoryId++) // loop the inventory
        {
            if (itemList[itemInventoryId].id == itemid)
                return true;
        }

        return false;
    }
}
