using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DigitalRuby.PyroParticles;

public class PlayerAttack : MonoBehaviour
{
    public bool isAttacking = false;
    public emitPoint emitPoint;
    public EnemyStatus es;
    public GameObject arrawRain;
    public GameObject[] Prefabs;
    private GameObject currentPrefabObject;
    private FireBaseScript currentPrefabScript;
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
            if (pa.job == PlayerAttribute.Classes.Archer)
            {
                AttackByShoot();
                //AttackByTripleShoot();
                //AttackByArrowRain();
                //AttackByJumpShoot();
            }
            if (pa.job == PlayerAttribute.Classes.Magician)
            {
                //AttackByShoot();    //stone ball
                AttackByFire(0); //depends 0 small, 1 big, 2 fire rain
                isAttacking = false;
            }

        }
    }

    public void UseSkill(int skillIndex, int skillLevel)
    {
        switch (pa.job)
        {
            case PlayerAttribute.Classes.Archer:
                switch (skillIndex)
                {
                    case 1:
                        AttackByTripleShoot();
                        break;
                    case 2:
                        AttackByJumpShoot();
                        break;
                    case 3:
                        AttackByArrowRain();
                        break;
                }
                break;
            case PlayerAttribute.Classes.Magician:
                switch (skillIndex)
                {
                    case 1:
                        AttackByFire(1);
                        break;
                    case 2:
                        AttackByShoot();
                        break;
                    case 3:
                        AttackByFire(2);
                        break;
                }
                break;
            case PlayerAttribute.Classes.Warrior:
                switch (skillIndex)
                {
                    case 1:
                        AttackByStrike(skillLevel);
                        break;
                    case 2:
                        AttackByStrong(skillLevel);
                        break;
                    case 3:
                        AttackByCyclone(skillLevel);
                        break;
                }
                break;
        }
    }

    //below for warrior
    void Attack()
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
                ea.TakeDamage(GetShortRangeDamage(pa.atk, 0, ea.defence));
                es.UpdateUI(ea);
            }
        }
        timer = 0f;
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("SwordAttack");
        anim.SetTrigger("Attack");
    }

    void AttackByCyclone(int level)
    {
        pa.ConsumeMP(50);
        Collider[] hitColliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), 3);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.tag == "Enemy")
            {
                EnemyAttribute ea = hitColliders[i].transform.gameObject.GetComponent<EnemyAttribute>();
                ea.TakeDamage(GetShortRangeDamage(pa.atk, Mathf.FloorToInt(pa.atk * (level / 10)), ea.defence));
                es.UpdateUI(ea);
            }
            i++;
        }
        timer = 0f;
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("SwordAttack");
        anim.SetTrigger("AttackBySkill0");
    }

    void AttackByStrike(int level)
    {
        pa.ConsumeMP(20);
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
                ea.TakeDamage(GetShortRangeDamage(pa.atk, Mathf.FloorToInt(pa.atk * (0.5f + level/10)), ea.defence));
                es.UpdateUI(ea);
            }
        }
        timer = 0f;
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("SwordAttack");
        anim.SetTrigger("AttackBySkill1");
    }

    void AttackByStrong(int level)
    {
        pa.ConsumeMP(30);
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
                ea.TakeDamage(GetShortRangeDamage(pa.atk, Mathf.FloorToInt(pa.atk * (0.5f + level / 10)), ea.defence));
                es.UpdateUI(ea);
            }
        }
        timer = 0f;
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("SwordAttack");
        anim.SetTrigger("AttackBySkill2");
    }

    //below for archer
    void AttackByShoot()
    {
        emitPoint.AttackByShoot();
        timer = 0f;
        anim.SetTrigger("AttackByShoot");
    }

    //Archer skill 2
    void AttackByJumpShoot()
    {
        emitPoint.AttackByJumpShoot();
        timer = 0f;
        anim.SetTrigger("AttackBySkill1");
    }

    //Archer skill 1
    void AttackByTripleShoot()
    {
        emitPoint.AttackByTripleShoot();
        timer = 0f;
        anim.SetTrigger("AttackBySkill2");
    }

    //Archer skill 3
    void AttackByArrowRain()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 origin = transform.position + new Vector3(0.0f, 1.0f, 0.0f); ;
        RaycastHit hit;
        if (Physics.Raycast(origin, forward, out hit, 15))
        {
            //Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
            //Debug.Log(hit.transform.name);
            if (hit.transform.gameObject.tag == "Enemy")
            {
                GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("ArcheryAttack");
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

    //below for magician
    void AttackByFire(int skillNum)
    {
        Vector3 pos;
        float yRot = transform.rotation.eulerAngles.y;
        Vector3 forwardY = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 up = transform.up;
        Quaternion rotation = Quaternion.identity;
        currentPrefabObject = GameObject.Instantiate(Prefabs[skillNum]);
        currentPrefabScript = currentPrefabObject.GetComponent<FireConstantBaseScript>();

        if (currentPrefabScript == null)
        {
            // temporary effect, like a fireball
            currentPrefabScript = currentPrefabObject.GetComponent<FireBaseScript>();
            if (currentPrefabScript.IsProjectile)
            {
                // set the start point near the player
                rotation = transform.rotation;
                pos = transform.position + forward + right + up;
            }
            else
            {
                // set the start point in front of the player a ways
                pos = transform.position + (forwardY * 10.0f);
            }
        }
        else
        {
            // set the start point in front of the player a ways, rotated the same way as the player
            pos = transform.position + (forwardY * 5.0f);
            rotation = transform.rotation;
            pos.y = 0.0f;
        }

        FireProjectileScript projectileScript = currentPrefabObject.GetComponentInChildren<FireProjectileScript>();
        if (projectileScript != null)
        {
            // make sure we don't collide with other fire layers
            projectileScript.ProjectileCollisionLayers &= (~UnityEngine.LayerMask.NameToLayer("FireLayer"));
        }

        currentPrefabObject.transform.position = pos;
        currentPrefabObject.transform.rotation = rotation;

    }

    public void AttackEnd()
    {
        isAttacking = false;
    }

    public int GetLongRangeDamage(int playerAtkVal, int skillDamage, int enemyDefense, int distance)
    {
        return Mathf.Max((Mathf.CeilToInt((playerAtkVal + skillDamage) * 0.5f) - enemyDefense), (((playerAtkVal + skillDamage) * (1 - Mathf.Abs(distance - 10) / 10)) - enemyDefense));
    }

    public int GetShortRangeDamage(int playerAtkVal, int skillDamage, int enemyDefense)
    {
        return (playerAtkVal + skillDamage) - enemyDefense;
    }
}
