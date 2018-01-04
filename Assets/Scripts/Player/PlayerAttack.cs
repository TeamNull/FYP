using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public int damage = 20;
    public float attackSpeed = 1f;
    public bool isAttacking = false;

    float timer = 0f;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= attackSpeed) {
            isAttacking = true;
            Attack();
        }
	}

    void Attack() {
        timer = 0f;
        anim.SetTrigger("Attack");
    }
}
