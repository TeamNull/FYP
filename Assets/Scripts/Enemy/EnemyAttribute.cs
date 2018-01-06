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

    bool isDead = false;
    Animator anim;
    UnityEngine.AI.NavMeshAgent nav;
    Transform playerPosition;
    PlayerAttribute pa;
    float timer;

    // Use this for initialization
    void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Vector3.Distance(playerPosition.position, transform.position) < 2) {
            nav.enabled = false;
            anim.SetBool("IsMoving", false);
            if (timer >= attackSpeed)
            {
                anim.SetTrigger("Attack");
                timer = 0;
            }
        }
        else if (Vector3.Distance(playerPosition.position, transform.position) < 6)
        {
            nav.enabled = true;
            anim.SetBool("IsMoving", true);
            nav.SetDestination(playerPosition.position);
            transform.LookAt(playerPosition);
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
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerAttribute>().GainExp(exp);
            isDead = true;
            anim.SetTrigger("Dead");
            Destroy(this.gameObject, destoryDelay);
        }
    }
}
