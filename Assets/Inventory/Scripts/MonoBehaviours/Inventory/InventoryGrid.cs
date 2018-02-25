using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryGrid : MonoBehaviour {

    int gridId;
    Inventory inventory;

	// Use this for initialization
	void Start () {
        inventory = this.transform.parent.gameObject.GetComponent<Inventory>();
        gridId = int.Parse(name);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
            inventory.RemoveItem(gridId);
	}
}
