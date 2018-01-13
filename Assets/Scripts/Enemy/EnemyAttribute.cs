using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttribute : MonoBehaviour
{

    public int startingHealth = 100;
    public int currentHealth;
    public int exp = 10;
    public float destoryDelay = 1.0f;
    public float attackSpeed = 1.0f;
    public int attack = 10;
    public int currentLevel;

    bool isDead = false;
    Animator anim;
    UnityEngine.AI.NavMeshAgent nav;
    GameObject player;
    float timer;
    Vector3 spawnPoint;
    Quaternion spawnQuaternion;
    EnemyManager em;

    // Use this for initialization
    void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        spawnPoint = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        spawnQuaternion = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w);
        em = GameObject.FindGameObjectWithTag("Manager").GetComponent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (StaticVarAndFunction.PlayerIsDead) return;
        timer += Time.deltaTime;
        if (Vector3.Distance(player.transform.position, transform.position) < 2)
        {
            nav.enabled = false;
            anim.SetBool("IsMoving", false);
            if (timer >= attackSpeed)
            {
                anim.SetTrigger("Attack");
                player.GetComponent<PlayerAttribute>().TakeDamge(attack);
                timer = 0;
            }
        }
        else if (Vector3.Distance(player.transform.position, transform.position) < 6)
        {
            nav.enabled = true;
            anim.SetBool("IsMoving", true);
            nav.SetDestination(player.transform.position);
            transform.LookAt(player.transform.position);
        }
        else
        {
            nav.enabled = false;
            anim.SetBool("IsMoving", false);
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;
        anim.SetTrigger("Damage");

        if (currentHealth <= 0)
        {
            
            isDead = true;
            anim.SetTrigger("Dead");
            player.GetComponent<PlayerAttribute>().GainExp(exp, currentLevel);
            em.Spawn(spawnPoint, spawnQuaternion);
            Destroy(this.gameObject, destoryDelay);
        }
    }
}
