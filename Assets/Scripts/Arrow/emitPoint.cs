﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emitPoint : MonoBehaviour {

    public int theRange = 1000;
    public GameObject theArrow;
    Vector3 theEmitPoint = Vector3.zero;
    Vector3 theDirection = Vector3.zero;
    Vector3 theTargetPoint = Vector3.zero;
    
    //RaycastHit theHit;


    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        theEmitPoint = this.transform.position;
        theDirection = this.transform.TransformDirection(Vector3.forward);        

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            //set anim
            GameObject cloneArrow=Instantiate(theArrow, theEmitPoint, Quaternion.identity);
            //GameObject cloneArrow = Instantiate(theArrow, theEmitPoint, this.transform.rotation);
            cloneArrow.transform.position = theEmitPoint;
            cloneArrow.transform.rotation = transform.rotation;
        }
    }

    
}
