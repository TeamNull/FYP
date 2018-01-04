using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttribute : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public int exp = 10;
    public float sinkSpeed = 2.5f;

    bool isDead = false;
    Animator anim;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

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
        }
    }

    public void StartSinking()
    {
        //GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        Destroy(gameObject, 2f);
    }
}
