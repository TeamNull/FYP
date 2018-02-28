using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Bag : MonoBehaviour
{
    const int itemSlotsNum = 24; //total solts
    public List<Item> itemList = new List<Item>(); //testing use
    public List<Image> itemImages = new List<Image>(); //show in ui

    int itemInBag;

    private void Awake()
    {
        StaticVarAndFunction.bag = this;
    }

    private void Start()
    {
        List<Transform> childs = new List<Transform>();
        // GetComponentsInChildren does not guarantee the order so do the sorting by ourselves
        for (int i = 0; i < itemSlotsNum; i++)  
        {
            childs.Add(transform.GetChild(i));
        }
        foreach(Transform child in childs) {
            itemImages.Add(child.GetChild(0).GetComponent<Image>());
        }
        itemImages.OrderBy(x => x.name);
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

    public void RemoveItem(int bagId)
    {
        if (itemInBag == 0 || bagId + 1 > itemInBag) return;

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
                itemImages[i].sprite = itemList[i].sprite;
                itemImages[i].enabled = true;
            } else {
                itemImages[i].sprite = null;
                itemImages[i].enabled = false;
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


