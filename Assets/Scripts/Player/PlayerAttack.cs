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

        if (Input.GetButton("Fire2") && timer >= pa.attackSpeed && !StaticVarAndFunction.PlayerIsDead && !StaticVarAndFunction.isLoading && !EventSystem.current.IsPointerOverGameObject())
        {
            isAttacking = true;
            AttackByShoot();
        }

        /*if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Archer_attack")) {
            isAttacking = false;
        }*/
    }

    void Attack() {
        Ray ray = Camera.main.ScreenPointToRay(transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
            //Debug.Log(hit.transform.name);
            if (hit.distance <= 2)
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    hit.transform.gameObject.GetComponent<EnemyAttribute>().TakeDamage(pa.atk);

                }
            }
        }
        timer = 0f;
        anim.SetTrigger("Attack");
    }

    void AttackByShoot() {
        timer = 0f;
        emitPoint.AttackByShoot();
        anim.SetTrigger("AttackByShoot");
    }

    public void AttackEnd() {
        isAttacking = false;
    }
}
