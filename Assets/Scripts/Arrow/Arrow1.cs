using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow1 : MonoBehaviour {
    public static int arrowCounter=0;
    public int theSpeed=50;
    Rigidbody ridigB;
    bool isColl=false;
    // Use this for initialization
    void Start () {
        //this.transform.rotation;
        ridigB = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {        
        if(!isColl)transform.Translate(Vector3.left* theSpeed*Time.deltaTime);
	}

    void OnTriggerEnter(Collider coll)
    {
        Debug.Log("get inside anything");
        ridigB.velocity = Vector3.zero;
        ridigB.useGravity = false;
        ridigB.isKinematic = true;
        isColl = true;
        Debug.Log("arrow"+ ++arrowCounter);
        Destroy(this);
    }
}
