using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emitPoint : MonoBehaviour {

    public int theRange = 15;
    public GameObject theArrow;
    Vector3 theEmitPoint = Vector3.zero;
    //Vector3 theDirection = Vector3.zero;
    //Vector3 theTargetPoint = Vector3.zero;
    PlayerAttack playerAttack;
    GameObject player;
    //RaycastHit theHit;


    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        theEmitPoint = transform.position;
        //theDirection = transform.TransformDirection(Vector3.forward);       

    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            //set anim
            GameObject cloneArrow=Instantiate(theArrow, theEmitPoint, Quaternion.identity);
            //GameObject cloneArrow = Instantiate(theArrow, theEmitPoint, this.transform.rotation);
            cloneArrow.transform.position = theEmitPoint;
            cloneArrow.transform.rotation = transform.rotation;
        }
        */
    }

    public void AttackByShoot() {
        player = GameManager.player;
        playerAttack = player.GetComponent<PlayerAttack>();
        GameObject cloneArrow = Instantiate(theArrow, theEmitPoint, Quaternion.identity);        
        cloneArrow.transform.position = theEmitPoint;
        cloneArrow.transform.rotation = transform.rotation;
        //cloneArrow.transform.eulerAngles = new Vector3(0,270,0);
        playerAttack.isAttacking = false;
    }
    
}
