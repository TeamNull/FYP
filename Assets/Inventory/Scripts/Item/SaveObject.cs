using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SaveObject : MonoBehaviour
{

    public Item item;
    Bag bag;
    public int unit = 1;

    void Start()
    {
        //bag = StaticVarAndFunction.bag;
    }

    void Update()
    {
        if (GameManager.mainCamRendered)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
            {
                //Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
                //Debug.Log(hit.transform.name);
                if (hit.distance <= 50)
                {
                    //Debug.Log(hit.transform.gameObject.name);
                    if (hit.transform.gameObject == this.gameObject)
                    {
                        if (item.id != 0)
                        {
                            bag = GameManager.bag;
                            bag.AddItem(item, unit);
                        }
                        else
                        {
                            item.unit += unit;
                            GameManager.inGameLog.AddLog("You have earned " + unit + " coin.", Color.yellow);
                        }
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
