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
    GameObject player;

    void Start()
    {
        //bag = StaticVarAndFunction.bag;
        player = StaticVarAndFunction.player;
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
                    if (item.id != 0)
                    {
                        bag = StaticVarAndFunction.bag;
                        bag.AddItem(item, unit);
                        player.GetComponent<MissionSystem>().Missiontype3(item.id);
                    }
                    else
                    {
                        item.unit += unit;
                    }
                    Destroy(this.gameObject);
                }
            }
        }

    }
}
