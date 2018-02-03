using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveObject : MonoBehaviour {

    public Item item;
    Inventory bag;
    public int unit = 1;

    void Start()
    {
        GameObject[] uiGameObjectArray = SceneManager.GetSceneByName("UI").GetRootGameObjects();
        foreach (GameObject go in uiGameObjectArray)
        {
            if (go.name == "PlayerUI")
            {
                bag = go.transform.Find("Bag").gameObject.GetComponent<Inventory>();
            }
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
        {
            //Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
            //Debug.Log(hit.transform.name);
            if (hit.distance <= 5)
            {
                if (hit.transform.gameObject == this.gameObject)
                {
                    bag.AddItem(item, unit);
                    Destroy(this.gameObject);
                }
            }
        }

    }
}
