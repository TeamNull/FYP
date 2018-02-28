using UnityEngine;

public class saveCoin1 : MonoBehaviour
{
	public Item item;
    public Bag bag;

	void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
		{
			//Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
			//Debug.Log(hit.transform.name);
			if (hit.distance <= 5) {
				if (hit.transform.name == "Coin (1)") {
					savecoin1 ();
				}
			}
		}

	}

	void savecoin1()
	{
		//bag.itemList[0] = item;
		//inventory.AddItem(0);
		Destroy(gameObject);
	}
}


