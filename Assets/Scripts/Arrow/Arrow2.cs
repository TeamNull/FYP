using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Arrow2 : MonoBehaviour {
    public int damage = 20;
    public EnemyStatus es;
    public float controlTimer = 0.0f;
    GameObject player;
    PlayerAttribute playerAttribute;
    Rigidbody ridigB;

    bool isColl = false;
    float timer = 0f;

    // Use this for initialization
    void Start () {
        ridigB = GetComponent<Rigidbody>();
        es = SceneManager.GetSceneByName("UI").GetRootGameObjects()[0].transform.GetChild(11).GetComponent<EnemyStatus>();
    }

    private void Awake()
    {
        player = GameManager.player;
        playerAttribute = player.GetComponent<PlayerAttribute>();
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if (timer >= controlTimer)
        {
            if (!isColl) ridigB.useGravity = true; 

            //if (Vector3.Distance(initPos, transform.position) > range) Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Enemy" && playerAttack.isAttacking)
        if (other.tag == "Enemy")
        {
            EnemyAttribute temp = other.gameObject.GetComponent<EnemyAttribute>();
            temp.TakeDamage(damage + playerAttribute.atk);
            es.UpdateUI(temp);
            Destroy(gameObject);
            return;
        }
        if (other.tag == "Player"|| other.tag == "Arrow")
            return;

        ridigB.velocity = Vector3.zero;
        ridigB.useGravity = false;
        //ridigB.isKinematic = true;
        isColl = true;
        Destroy(gameObject, 1f);

    }
}
