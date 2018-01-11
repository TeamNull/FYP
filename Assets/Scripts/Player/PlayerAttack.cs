using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    public bool isAttacking = false;

    float timer = 0f;
    Animator anim;
    PlayerAttribute pa;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        pa = GetComponent<PlayerAttribute>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= pa.attackSpeed && !StaticVarAndFunction.PlayerIsDead) {
            isAttacking = true;
            Attack();
        }
	}

    void Attack() {
        timer = 0f;
        anim.SetTrigger("Attack");
    }

    public void AttackEnd() {
        isAttacking = false;
    }
}
