﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

    Transform standardPos;          // Camera normal positionLIHKG

	// Use this for initialization
	void Start () {
        standardPos = GameObject.Find("MinimapCamPos").transform;

        transform.position = standardPos.position;
        transform.forward = standardPos.forward;
        transform.rotation = standardPos.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = standardPos.position;
        transform.forward = standardPos.forward;
	}
}