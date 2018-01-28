using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow1 : MonoBehaviour {
    public static int arrowCounter=0;
    public int theSpeed=50;
    public int damage = 20;
    public int range = 100;

    GameObject player;
    PlayerAttack playerAttack;
    PlayerAttribute playerAttribute;
    Vector3 initPos;
    Rigidbody ridigB;

    bool isColl=false;

    // Use this for initialization
    void Start () {
        //this.transform.rotation;
        ridigB = GetComponent<Rigidbody>();
        initPos = transform.position;
    }

    private void Awake()
    {
        player = StaticVarAndFunction.player;
        playerAttack = player.GetComponent<PlayerAttack>();
        playerAttribute = player.GetComponent<PlayerAttribute>();
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
        }       

        ridigB.velocity = Vector3.zero;
        ridigB.useGravity = false;
        ridigB.isKinematic = true;
        isColl = true;        
        Destroy(gameObject,3);

    }
}
