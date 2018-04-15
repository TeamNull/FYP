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
    public GameObject arrawRain;

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

        if (Input.GetButton("Fire1") && timer >= pa.attackSpeed && !GameManager.PlayerIsDead && !GameManager.isLoading && !EventSystem.current.IsPointerOverGameObject())
        {
            isAttacking = true;
            if (pa.job == PlayerAttribute.Classes.Warrior)
            {
                Attack();
            }
            else
            {
                AttackByArrowRain();
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
        emitPoint.AttackByShoot();        
        timer = 0f;
        anim.SetTrigger("AttackByShoot");
    }

    void AttackByArrowRain()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 origin = transform.position + new Vector3(0.0f, 1.0f, 0.0f); ;
        RaycastHit hit;
        if (Physics.Raycast(origin, forward, out hit, 2))
        {
            //Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
            //Debug.Log(hit.transform.name);
            if (hit.transform.gameObject.tag == "Enemy")
            {
                //EnemyAttribute ea = hit.transform.gameObject.GetComponent<EnemyAttribute>();
                //ea.TakeDamage(pa.atk);
                //es.UpdateUI(ea);

                //creat arrow rain at the head of monster x,z fix y10
                GameObject obj = Instantiate(arrawRain, hit.transform.gameObject.transform.position+new Vector3(0, 10, 0), hit.transform.gameObject.transform.rotation);
                //arrawRain.transform.position = hit.transform.gameObject.transform.position;
                //arrawRain.transform.position += new Vector3(0,10,0);
                //arrawRain.SetActive(true);
                Destroy(obj, 5f);
            }
        }
        timer = 0f;
        anim.SetTrigger("Attack");
        //anim attack by skill 1
    }

    public void AttackEnd()
    {
        isAttacking = false;
    }

    public void UseSkill(int skillNumber) {
        
    }
}
