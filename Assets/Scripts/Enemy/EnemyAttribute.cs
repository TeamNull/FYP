using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttribute : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public int exp = 10;
    public float destoryDelay = 1.0f;

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
            Destroy(this.gameObject, destoryDelay);
        }
    }
}
