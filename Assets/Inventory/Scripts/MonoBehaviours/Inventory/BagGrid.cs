using System.Collections;
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
        bag = StaticVarAndFunction.bag;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData) {
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
            popUp.GetComponentInChildren<Text>().text = item.description;
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
