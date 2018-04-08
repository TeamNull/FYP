using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BagGrid : MonoBehaviour, IPointerClickHandler {

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
}
