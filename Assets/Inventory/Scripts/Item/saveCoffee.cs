using UnityEngine;

public class saveCoffee : MonoBehaviour
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
				if (hit.transform.name == "coffee") {
					savecoffee();
				}
			}
		}

	}

	void savecoffee()
	{
		//bag.itemList[1] = item;
		//inventory.AddItem(1);
		Destroy(gameObject);

	}
}


