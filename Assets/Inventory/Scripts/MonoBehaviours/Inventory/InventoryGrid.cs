using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryGrid : MonoBehaviour, IPointerClickHandler {

    int gridId;
    Inventory inventory;

    // Use this for initialization
    void Start()
    {
        gridId = int.Parse(name);
        inventory = StaticVarAndFunction.inventory;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log(gridId);
            inventory.RemoveItem(gridId);
        }
    }
}
