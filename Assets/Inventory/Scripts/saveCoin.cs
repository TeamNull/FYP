using UnityEngine;


	public class saveCoin : MonoBehaviour
	{
		public Item item;
		public Inventory inventory;

		void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
		{
			//Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
			//Debug.Log(hit.transform.name);
			if (hit.transform.name == "Coin") {
				savecoin ();
			}
		}

		}

		void savecoin()
		{
			inventory.AddItem(item);

			Debug.Log ("this is coin");

			Destroy(gameObject);

			Debug.Log ("coin is destroyed");
		}
	}


