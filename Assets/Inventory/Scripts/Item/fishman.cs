using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishman : MonoBehaviour {

	public Item item;
	public Inventory inventory;

	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
		{
			//Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
			//Debug.Log(hit.transform.name);
			if (hit.distance <= 5) {
				if (hit.transform.name == "Sharkman") {
					Fishman ();
				}
			}
		}

	}

	void Fishman()
	{
		Debug.Log (inventory.itemlist.Length);
		inventory.itemlist[2] = item;
		inventory.AddItem(2);
		Destroy(gameObject);
	}
}
