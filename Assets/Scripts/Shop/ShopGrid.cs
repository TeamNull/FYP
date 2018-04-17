using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopGrid : MonoBehaviour
    , IPointerClickHandler
    , IPointerEnterHandler
    , IPointerExitHandler
{
    int gridId;
    public Item coin;
    public Item item;
    //public static int tempCount = 0;
    public GameObject popUp;
    public GameObject coinText;
    
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
        if (eventData.button == PointerEventData.InputButton.Right&& item!=null)
        {            
            //bag.RemoveItem(gridId);
            //check if enough money, if no nothing happen
            //if yes then paid by decrease coins number 
            //also add to the bag
            if (coin.unit >= item.price) {
                coin.unit -= item.price;
                //item.unit++;
                GameManager.bag.AddItem(item,1);
                coinText.GetComponent<Coin>().updateCoin();
            }                       
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item != null)
        {
            popUp = GameObject.FindGameObjectWithTag("PopUp");
            //popUp.GetComponentInChildren<Text>().text = item.description;
            popUp.transform.GetChild(0).GetComponent<Text>().text = item.itemName;
            popUp.transform.GetChild(2).GetComponent<Text>().text = item.description;
            popUp.transform.GetChild(4).GetComponent<Text>().text = item.price.ToString();
            popUp.transform.position = this.transform.position;
            popUp.transform.position += (new Vector3(80, -40, 0));
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popUp = GameObject.FindGameObjectWithTag("PopUp");
        popUp.transform.position += (new Vector3(10000, 10000, 0));
    }
}
