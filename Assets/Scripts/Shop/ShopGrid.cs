using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopGrid : MonoBehaviour, IPointerClickHandler
{
    int gridId;
    //Bag bag;
    public Item coin;
    public Item item;
    //public int price;

    // Use this for initialization
    void Start () {
        gridId = int.Parse(name);
        //bag = StaticVarAndFunction.bag;
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("asd");
            //bag.RemoveItem(gridId);
            //check if enough money, if no nothing happen
            //if yes then paid by decrease coins number 
            //also add to the bag
            if (coin.unit >= item.price) {
                coin.unit -= item.price;
                //item.unit++;
                StaticVarAndFunction.bag.AddItem(item,1);
            }                       
        }
    }
}
