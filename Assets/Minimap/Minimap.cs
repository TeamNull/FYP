using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

    float smooth = 10f;
    Transform standardPos;          // Camera normal positionLIHKG

	// Use this for initialization
	void Start () {
        
	}

    public void Init() {
        standardPos = GameObject.Find("MinimapCamPos").transform;

        transform.position = standardPos.position;
        transform.forward = standardPos.forward;
        transform.rotation = standardPos.rotation;
    }
	
	// Update is called once per frame
    void FixedUpdate () {
        if (standardPos != null)
        {
            transform.position = Vector3.Lerp(transform.position, standardPos.position, Time.fixedDeltaTime * smooth);
            transform.forward = Vector3.Lerp(transform.forward, standardPos.forward, Time.fixedDeltaTime * smooth);
        }
	}
}
