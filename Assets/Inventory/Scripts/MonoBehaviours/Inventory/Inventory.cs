using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour {

    const int itemSlotsNum = 54; //total solts
    List<Item> itemList = new List<Item>(); //testing use
    List<Transform> itemUIList = new List<Transform>();

    int itemInInventory;

    private void Awake()
    {
        GameManager.inventory = this;
    }

    private void Start()
    {
        // GetComponentsInChildren does not guarantee the order so do the sorting by ourselves
        for (int i = 0; i < itemSlotsNum; i++)
        {
            itemUIList.Add(transform.GetChild(i));
        }
        itemUIList.OrderBy(x => x.name);
    }

    public void AddItem(Item item, int unit)
    {
        if (itemInInventory == itemSlotsNum) return; //Check bag space

        for (int i = 0; i < itemInInventory; i++) // loop the inventory
        {
            if (itemList[i].id == item.id)  // find the item in inventory
            {
                itemList[i].UpdateUnit(unit);
                itemUIList[i].GetChild(1).GetComponent<Text>().text = item.unit.ToString();
                return;
            }
        }

        itemList.Add(item);
        itemList[itemInInventory].unit++;
        itemUIList[itemInInventory].GetChild(0).GetComponent<Image>().sprite = item.sprite;
        itemUIList[itemInInventory].GetChild(0).GetComponent<Image>().enabled = true;
        itemUIList[itemInInventory].GetChild(1).GetComponent<Text>().text = item.unit.ToString();
        itemUIList[itemInInventory].GetChild(1).GetComponent<Text>().enabled = true;
        itemInInventory++;
    }

    public void RemoveItem(int inventoryId)
    {
        if (itemInInventory == 0 || inventoryId + 1 > itemInInventory) return;

        itemList[inventoryId].unit--;
        if (itemList[inventoryId].unit == 0)
        {
            itemInInventory--;
            itemList.RemoveAt(inventoryId);
            UpdateImageList();
        }
    }

    void UpdateImageList()
    {
        for (int i = 0; i < itemSlotsNum; i++)
        {
            if (i < itemInInventory)
            {
                itemUIList[itemInInventory].GetChild(0).GetComponent<Image>().sprite = itemList[i].sprite;
                itemUIList[itemInInventory].GetChild(0).GetComponent<Image>().enabled = true;
                itemUIList[itemInInventory].GetChild(1).GetComponent<Text>().text = itemList[i].unit.ToString();
                itemUIList[itemInInventory].GetChild(1).GetComponent<Text>().enabled = true;
            }
            else
            {
                itemUIList[itemInInventory].GetChild(0).GetComponent<Image>().sprite = null;
                itemUIList[itemInInventory].GetChild(0).GetComponent<Image>().enabled = false;
                itemUIList[itemInInventory].GetChild(1).GetComponent<Text>().text = "0";
                itemUIList[itemInInventory].GetChild(1).GetComponent<Text>().enabled = false;
            }
        }
    }

    public bool Cotainitem(int itemId)
    {
        for (int itemInventoryId = 0; itemInventoryId < itemSlotsNum; itemInventoryId++) // loop the inventory
        {
            if (itemList[itemInventoryId].id == itemId)
                return true;
        }

        return false;
    }
}
