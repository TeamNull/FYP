using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{

    public bool isAttacking = false;
    public emitPoint emitPoint;
    public EnemyStatus es;

    float timer = 0f;
    Animator anim;
    PlayerAttribute pa;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        pa = GetComponent<PlayerAttribute>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= pa.attackSpeed && !StaticVarAndFunction.PlayerIsDead && !StaticVarAndFunction.isLoading && !EventSystem.current.IsPointerOverGameObject())
        {
            isAttacking = true;
            if (pa.job == PlayerAttribute.Classes.Warrior)
            {
                Attack();
            }
            else
            {
                AttackByShoot();
            }
        }
    }

    void Attack()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 origin = transform.position+ new Vector3(0.0f, 1.0f, 0.0f); ;
        RaycastHit hit;
        if (Physics.Raycast(origin, forward, out hit, 2))
        {
            //Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
            //Debug.Log(hit.transform.name);
            if (hit.transform.gameObject.tag == "Enemy")
            {
                EnemyAttribute ea = hit.transform.gameObject.GetComponent<EnemyAttribute>();
                ea.TakeDamage(pa.atk);
                es.UpdateUI(ea);
            }
        }
        timer = 0f;
        anim.SetTrigger("Attack");
    }

    void AttackByShoot()
    {
        timer = 0f;
        emitPoint.AttackByShoot();
        anim.SetTrigger("AttackByShoot");
    }

    public void AttackEnd()
    {
        isAttacking = false;
    }
}
