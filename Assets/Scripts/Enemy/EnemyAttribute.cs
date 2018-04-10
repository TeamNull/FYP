using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttribute : MonoBehaviour
{
    #region Variable
    public delegate void EnemyDeathHandler();
    public event EnemyDeathHandler EnemyDeath;
    public List<GameObject> DropItemList;
    public List<double> DropItemProbability;
    public Quaternion spawnQuaternion;
    public Vector3 spawnPoint;
    public Texture icon;
    public float destoryDelay = 1.0f;
    public float attackSpeed = 1.0f;
    public int startingHealth = 100;
    public int startingMp = 100;
    public int currentHealth;
    public int currentMp;
    public int exp = 10;
    public int attack = 10;
    public int currentLevel;
    public bool isDead;

    Animator anim;
    UnityEngine.AI.NavMeshAgent nav;
    GameObject player;
    float timer;
    #endregion

    #region Life Cycle
    // Use this for initialization
    void Start()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        player = StaticVarAndFunction.player;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        spawnPoint = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        spawnQuaternion = new Quaternion(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w);
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
    #endregion

    #region Method
    public void TakeDamage(int amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;
        anim.SetTrigger("Damage");

        if (currentHealth <= 0)
        {
            isDead = true;
            currentHealth = 0;
            anim.SetTrigger("Dead");
            player.GetComponent<PlayerAttribute>().GainExp(exp, currentLevel);
            if (this.gameObject.transform.name == "Vulture" || this.gameObject.transform.name == "VultureBoss")
            {
                player.GetComponent<MissionSystem>().Thebossmonsterisdead(this.gameObject.transform.name);
            }
            //print(this.gameObject.transform.name);
            player.GetComponent<MissionSystem>().Missiontype1(1, this.gameObject.transform.name);
            if (EnemyDeath != null) EnemyDeath();
            for (int i = 0; i < DropItemList.Count; i++) {
                float randomNumber = Random.Range(0.0f, 1.0f);
                if (DropItemProbability[i] > 1) {
                    DropItemProbability[i] = 1.0f;
                }

                if (randomNumber <= DropItemProbability[i]) {
                    Instantiate(DropItemList[i], transform.position, DropItemList[i].transform.rotation);
                }
            }
            this.gameObject.SetActive(false);
        }
    }
    #endregion
}
