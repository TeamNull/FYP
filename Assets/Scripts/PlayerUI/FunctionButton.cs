using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionButton : MonoBehaviour {

    public GameObject Attribute;    

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

}
