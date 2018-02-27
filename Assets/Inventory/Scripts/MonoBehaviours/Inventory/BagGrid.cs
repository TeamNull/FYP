using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagGrid : MonoBehaviour {

    int gridId;
    Bag bag;

	// Use this for initialization
	void Start () {
        bag = this.transform.parent.gameObject.GetComponent<Bag>();
        gridId = int.Parse(name);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
            bag.RemoveItem(gridId);
	}
}
