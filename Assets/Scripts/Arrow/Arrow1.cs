﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Arrow1 : MonoBehaviour {
    public static int arrowCounter=0;
    public int theSpeed=50;
    public int damage = 20;
    public int range = 100;
    public int a = 0;
    public int b = 0;
    public int c = 0;
    public EnemyStatus es;

    GameObject player;
    //PlayerAttack playerAttack;
    PlayerAttribute playerAttribute;
    Vector3 initPos;
    Rigidbody ridigB;

    bool isColl=false;

    // Use this for initialization
    void Start () {        
        ridigB = GetComponent<Rigidbody>();
        initPos = transform.position;
        transform.Rotate(new Vector3(a,b,c));
        es = SceneManager.GetSceneByName("UI").GetRootGameObjects()[0].transform.GetChild(11).GetComponent<EnemyStatus>();
    }

    private void Awake()
    {
        player = StaticVarAndFunction.player;        
        playerAttribute = player.GetComponent<PlayerAttribute>();
        //transform.rotation = Quaternion.LookRotation(ridigB.velocity);
    }

    // Update is called once per frame
    void Update () {        
        if(!isColl)transform.Translate(Vector3.left* theSpeed*Time.deltaTime);
        
        if (Vector3.Distance(initPos, transform.position) > range) Destroy(gameObject);

	}

    void OnTriggerEnter(Collider other)
    {     
        //if (other.tag == "Enemy" && playerAttack.isAttacking)
        if (other.tag == "Enemy")
        {
            EnemyAttribute temp = other.gameObject.GetComponent<EnemyAttribute>();
            temp.TakeDamage(damage + playerAttribute.atk);
            es.UpdateUI(temp);
            Destroy(gameObject);
            return;
        }       

        ridigB.velocity = Vector3.zero;
        ridigB.useGravity = false;
        ridigB.isKinematic = true;
        isColl = true;        
        Destroy(gameObject,1f);

    }
}
