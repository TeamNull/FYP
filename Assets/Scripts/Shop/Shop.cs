using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Shop : MonoBehaviour {

    const int itemSlotsNum = 24; //total solts
    public List<Item> itemList = new List<Item>(); //testing use
    public List<Transform> itemUIList = new List<Transform>();
    public Item[] goods;
    public Item coin;
    GameObject player;

    int itemInShop;

    private void Awake()
    {
        //StaticVarAndFunction.bag = this;
    }

    private void Start()
    {
        // GetComponentsInChildren does not guarantee the order so do the sorting by ourselves
        for (int i = 0; i < itemSlotsNum; i++)
        {
            itemUIList.Add(transform.GetChild(i));
            Destroy(transform.GetChild(i).gameObject.GetComponent<BagGrid>());
            transform.GetChild(i).gameObject.AddComponent<ShopGrid>();            
        }
        itemUIList.OrderBy(x => x.name);
        for (int i=0;i< goods.Length;i++) {
            itemUIList[i].GetChild(0).GetComponent<Image>().sprite = goods[i].sprite;
            itemUIList[i].GetChild(0).GetComponent<Image>().enabled = true;
            itemUIList[i].GetComponent<ShopGrid>().item = goods[i];
            itemUIList[i].GetComponent<ShopGrid>().coin = coin;
            //itemUIList[i].GetComponent<ShopGrid>().popUp = popUp;
        }
        this.gameObject.transform.parent.gameObject.SetActive(false);
    }

    


}
