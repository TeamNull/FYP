using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Arrow1 : MonoBehaviour {
    public int theSpeed=50;
    public int range = 100;
    public string arrowType;
    public int skillLevel;

    public EnemyStatus es;
    public float controlTimer = 0.0f;
    GameObject player;
    PlayerAttribute playerAttribute;
    Vector3 initPos;
    Rigidbody ridigB;

    bool isColl=false;
    float timer = 0f;

    // Use this for initialization
    void Start () {        
        ridigB = GetComponent<Rigidbody>();
        initPos = transform.position;
        transform.Rotate(new Vector3(0,0,0));
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
            if (!isColl) transform.Translate(Vector3.left * theSpeed * Time.deltaTime);

            if (Vector3.Distance(initPos, transform.position) > range) Destroy(gameObject);
        }
	}

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name+" "+other.tag);
        //if (other.tag == "Enemy" && playerAttack.isAttacking)
        if (other.tag == "Enemy")
        {
            
            EnemyAttribute temp = other.gameObject.GetComponent<EnemyAttribute>();
            float skillDamage = 0;
            switch (arrowType) {
                case "Normal":
                    skillDamage = 0;
                    break;
                case "Triple":
                    skillDamage = (skillLevel / 10) / 3;
                    break;
                case "Jump":
                    skillDamage = skillLevel / 10;
                    break;
            }
            temp.TakeDamage(player.GetComponent<PlayerAttack>().GetLongRangeDamage(playerAttribute.atk,Mathf.FloorToInt(playerAttribute.atk * skillDamage),temp.defence,Mathf.FloorToInt(Vector3.Distance(player.transform.position, temp.transform.position))));
            es.UpdateUI(temp);
            Destroy(gameObject);
            return;
        }

        if (other.tag == "Player")
            return;

        ridigB.velocity = Vector3.zero;
        ridigB.useGravity = false;
        ridigB.isKinematic = true;
        isColl = true;        
        Destroy(gameObject,1f);

    }
}
