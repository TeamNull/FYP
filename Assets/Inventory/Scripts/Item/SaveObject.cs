using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveObject : MonoBehaviour, IPointerClickHandler
{

    public Item item;
    Bag bag;
    public int unit = 1;

    void Start()
    {
        bag = StaticVarAndFunction.bag;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Debug.Log("Clicked");
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
                    if (item.id != 0)
                    {
                        bag.AddItem(item, unit);
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
