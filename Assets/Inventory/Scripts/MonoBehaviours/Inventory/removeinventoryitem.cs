using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeinventoryitem : MonoBehaviour {

	public Item item;
	public Bag inventory;

	void Update () {
		if (Input.GetKeyDown(KeyCode.F1))
		{
			deleteitem(0);
		}

		if (Input.GetKeyDown(KeyCode.F2))
		{
			deleteitem(1);
		}

		if (Input.GetKeyDown(KeyCode.F3))
		{
			deleteitem(2);
		}

		if (Input.GetKeyDown(KeyCode.F4))
		{
			deleteitem(3);
		}


	}

	void deleteitem(int iteminventoryid)
	{

		inventory.RemoveItem(iteminventoryid);

		//Debug.Log ("iteminventoryid: " + iteminventoryid);

	}
		
}
