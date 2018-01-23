using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAttack : MonoBehaviour {

    public bool isAttacking = false;
    public emitPoint emitPoint;


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

        if (Input.GetButton("Fire1") && timer >= pa.attackSpeed && !StaticVarAndFunction.PlayerIsDead && !StaticVarAndFunction.isLoading && !EventSystem.current.IsPointerOverGameObject()) {
            isAttacking = true;
            Attack();
        }

        if (Input.GetButton("Fire2") && timer >= pa.attackSpeed && !StaticVarAndFunction.PlayerIsDead && !StaticVarAndFunction.isLoading)
        {
            isAttacking = true;
            AttackByShoot();
        }


    }

    void Attack() {
        timer = 0f;
        anim.SetTrigger("Attack");
    }

    void AttackByShoot() {
        timer = 0f;
        emitPoint.AttackByShoot();
        anim.SetTrigger("AttackByShoot");
        
        //set anim later
        //AttackByShoot
    }

    public void AttackEnd() {
        isAttacking = false;
    }
}
