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
                //AttackByStrike();
                //AttackByCyclone();
                //AttackByStrong();
            }
            else
            {
                AttackByShoot();
                //AttackByTripleShoot();
                //AttackByArrowRain();
                //AttackByJumpShoot();
                //Attack();
            }
        }
    }

    //below for warrior
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

    void AttackByCyclone()
    {
        //Vector3 forward = transform.TransformDirection(Vector3.forward);
        //Vector3 origin = transform.position + new Vector3(0.0f, 1.0f, 0.0f); ;
        //RaycastHit hit;
        //if (Physics.OverlapSphere(origin, 2,transform.forward ,out hit, 2))
        //{
        //    //Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
        //    //Debug.Log(hit.transform.name);
        //    if (hit.transform.gameObject.tag == "Enemy")
        //    {
        //        EnemyAttribute ea = hit.transform.gameObject.GetComponent<EnemyAttribute>();
        //        ea.TakeDamage(pa.atk);
        //        es.UpdateUI(ea);
        //    }
        //}

        Collider[] hitColliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y +1.0f, transform.position.z), 3);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag == "Enemy")
            {
                EnemyAttribute ea = hitColliders[i].transform.gameObject.GetComponent<EnemyAttribute>();
                ea.TakeDamage(pa.atk);
                es.UpdateUI(ea);
            }
            i++;
        }
        timer = 0f;
        anim.SetTrigger("AttackBySkill0");
    }

    void AttackByStrike()
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
                EnemyAttribute ea = hit.transform.gameObject.GetComponent<EnemyAttribute>();
                ea.TakeDamage(pa.atk);
                es.UpdateUI(ea);
            }
        }
        timer = 0f;
        anim.SetTrigger("AttackBySkill1");
    }

    void AttackByStrong()
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
                EnemyAttribute ea = hit.transform.gameObject.GetComponent<EnemyAttribute>();
                ea.TakeDamage(pa.atk);
                es.UpdateUI(ea);
            }
        }
        timer = 0f;
        anim.SetTrigger("AttackBySkill2");
    }

    //below for archer
    void AttackByShoot()
    {      
        emitPoint.AttackByShoot();        
        timer = 0f;
        anim.SetTrigger("AttackByShoot");
    }

    void AttackByJumpShoot()
    {
        emitPoint.AttackByJumpShoot();
        timer = 0f;
        anim.SetTrigger("AttackBySkill1");
    }

    void AttackByTripleShoot()
    {
        emitPoint.AttackByTripleShoot();
        timer = 0f;
        anim.SetTrigger("AttackBySkill2");
    }

    void AttackByArrowRain()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 origin = transform.position + new Vector3(0.0f, 1.0f, 0.0f); ;
        RaycastHit hit;
        if (Physics.Raycast(origin, forward, out hit,15))
        {
            //Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
            //Debug.Log(hit.transform.name);
            if (hit.transform.gameObject.tag == "Enemy")
            {
                //creat arrow rain at the head of monster x,z fix y10
                float tempX = (this.transform.position.x + hit.transform.position.x) / 2;
                float tempZ = (this.transform.position.z + hit.transform.position.z) / 2;
                //float tempX = hit.transform.position.x;
                //float tempZ = hit.transform.position.z;
                GameObject obj = Instantiate(arrawRain, new Vector3(tempX, 3, tempZ), hit.transform.gameObject.transform.rotation);
                //anim.SetTrigger("AttackBySkill0");
                Destroy(obj, 5f);
                timer = 0f;
                anim.SetTrigger("AttackBySkill0");
            }           
        }
    }



    public void AttackEnd()
    {
        isAttacking = false;
    }

    public void UseSkill(int skillNumber) {
        
    }
}
