﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BagGrid : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    , IPointerExitHandler
{

    public GameObject popUp;
    public Item item;
    int gridId;
    Bag bag;

	// Use this for initialization
	void Start () {
        gridId = int.Parse(name);
        bag = GameManager.bag;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData) {
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("ClickButton");
        if (eventData.button == PointerEventData.InputButton.Right) {
            Debug.Log(gridId);
            bag.RemoveItem(gridId);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            popUp = GameObject.FindGameObjectWithTag("PopUp");
            //popUp.GetComponentInChildren<Text>().text = item.description;
            popUp.transform.GetChild(0).GetComponent<Text>().text= item.itemName;
            popUp.transform.GetChild(2).GetComponent<Text>().text = item.description;
            popUp.transform.GetChild(4).GetComponent<Text>().text = (item.price*0.2).ToString();
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
