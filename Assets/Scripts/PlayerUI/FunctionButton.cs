using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionButton : MonoBehaviour {

    public GameObject Attribute;
    //public MeshRenderer AttributeRender;

    // Use this for initialization
    void Start () {
        //MeshRenderer render = Attribute.GetComponentInChildren<MeshRenderer>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EnableAttribute() {
        Debug.Log("enable att");
        //Attribute.SetActive(false);
        //AttributeRender.enabled = !AttributeRender.enabled;
        if (Attribute.activeSelf) Attribute.SetActive(false); else Attribute.SetActive(true);
    }

}
