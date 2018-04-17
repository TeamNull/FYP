using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bigmap : MonoBehaviour {

    Transform standardPos;          // Camera normal positionLIHKG
    
    // Use this for initialization
    void Start () {
		
	}

    public void Init()
    {
        standardPos = GameObject.Find("BigmapCamPos").transform;

        transform.position = standardPos.position;
        transform.forward = standardPos.forward;
        transform.rotation = standardPos.rotation;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
