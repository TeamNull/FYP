using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PopUp : MonoBehaviour, IPointerEnterHandler
    , IPointerExitHandler
{
    GameObject popUp;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        
            popUp = GameObject.FindGameObjectWithTag("PopUp");
            //popUp.GetComponentInChildren<Text>().text = item.description;
            popUp.transform.position = this.transform.position;
            popUp.transform.position += (new Vector3(130, -20, 0));
        

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popUp = GameObject.FindGameObjectWithTag("PopUp");
        popUp.transform.position += (new Vector3(10000, 10000, 0));
    }
}
