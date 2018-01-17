using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionButton : MonoBehaviour {

    public GameObject Attribute;
    public GameObject DragDropTest;

    // Use this for initialization
    void Start () {        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnableAttribute() {
        if (Attribute.activeSelf){
            Attribute.SetActive(false);
        }else {
            Attribute.SetActive(true);
        }        
    }

    public void EnableDragDropTest()
    {
        if (DragDropTest.activeSelf)
        {
            DragDropTest.SetActive(false);
        }
        else
        {
            DragDropTest.SetActive(true);
        }
    }

}
