using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword1 : MonoBehaviour {

    public int damage = 20;

    GameObject player;
    PlayerAttack playerAttack;
    PlayerAttribute playerAttribute;

    private void Awake()
    {
        player = GameManager.player;
        playerAttack = player.GetComponent<PlayerAttack>();
        playerAttribute = player.GetComponent<PlayerAttribute>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && playerAttack.isAttacking)
        {
            EnemyAttribute temp = other.gameObject.GetComponent<EnemyAttribute>();
            temp.TakeDamage(damage + playerAttribute.atk);
            playerAttack.isAttacking = false;
        }
    }
}
