using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Bag : MonoBehaviour
{
    const int itemSlotsNum = 24; //total solts
    List<Item> itemList = new List<Item>(); //testing use
    List<Transform> itemUIList = new List<Transform>();

    int itemInBag;

    private void Awake()
    {
        StaticVarAndFunction.bag = this;
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
        if (itemInBag == itemSlotsNum) return; //Check bag space

        for (int i = 0; i < itemInBag; i++) // loop the inventory
        {
            if (itemList[i].id == item.id)  // find the item in inventory
            {
                itemList[i].UpdateUnit(unit);
                itemUIList[i].GetChild(1).GetComponent<Text>().text = item.unit.ToString();
                return;
            }
        }

        itemList.Add(item);
        itemList[itemInBag].unit++;
        itemUIList[itemInBag].GetChild(0).GetComponent<Image>().sprite = item.sprite;
        itemUIList[itemInBag].GetChild(0).GetComponent<Image>().enabled = true;
        itemUIList[itemInBag].GetChild(1).GetComponent<Text>().text = item.unit.ToString();
        itemUIList[itemInBag].GetChild(1).GetComponent<Text>().enabled = true;
        itemInBag++;
    }

    public void RemoveItem(int bagId)
    {
        if (itemInBag == 0 || bagId + 1 > itemInBag) return;

        if (StaticVarAndFunction.inventory.transform.parent.gameObject.activeSelf == true) {
            StaticVarAndFunction.inventory.AddItem(itemList[bagId], 1);
        }

        itemList[bagId].unit--;
        if (itemList[bagId].unit == 0)
        {
            itemInBag--;
            itemList.RemoveAt(bagId);
            UpdateImageList();
        }
    }

    void UpdateImageList()
    {
        for (int i = 0; i < itemSlotsNum; i++) {
            if (i < itemInBag) {
                itemUIList[itemInBag].GetChild(0).GetComponent<Image>().sprite = itemList[i].sprite;
                itemUIList[itemInBag].GetChild(0).GetComponent<Image>().enabled = true;
                itemUIList[itemInBag].GetChild(1).GetComponent<Text>().text = itemList[i].unit.ToString();
                itemUIList[itemInBag].GetChild(1).GetComponent<Text>().enabled = true;
            } else {
                itemUIList[itemInBag].GetChild(0).GetComponent<Image>().sprite = null;
                itemUIList[itemInBag].GetChild(0).GetComponent<Image>().enabled = false;
                itemUIList[itemInBag].GetChild(1).GetComponent<Text>().text = "0";
                itemUIList[itemInBag].GetChild(1).GetComponent<Text>().enabled = false;
            }
        }
    }

    public bool Cotainitem(int itemId)
    {
        for (int itemBagId = 0; itemBagId < itemSlotsNum; itemBagId++) // loop the inventory
        {
            if (itemList[itemBagId].id == itemId)
                return true;
        }

        return false;
    }
}


