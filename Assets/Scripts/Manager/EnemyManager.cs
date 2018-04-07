using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Variable
    public Transform[] spawnPoint;
    public Transform[] bossSpawnPoint;
    public GameObject enemy;
    public GameObject Boss;
    public int respwanTime = 5;

    List<GameObject> enemyResourcePool;
    //List<GameObject> bossResourcePool;
    #endregion

    #region Life Cycle
    // Use this for initialization
    void Start()
    {
        Init();

        //foreach (Transform t in bossSpawnPoint) {
        //    GameObject obj = Instantiate(enemy, t.position, t.rotation);
        //    bossResourcePool.Add(obj);
        //}
    }

    public void Init() {
        enemyResourcePool = new List<GameObject>();
        foreach (Transform t in spawnPoint)
        {
            GameObject obj = Instantiate(enemy, t.position, t.rotation);
            obj.GetComponent<EnemyAttribute>().EnemyDeath += Spawn;
            enemyResourcePool.Add(obj);
            Debug.Log("asd");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Method

    void Spawn() {
        StartCoroutine(Respawn());
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respwanTime);
        for (int i = 0; i < enemyResourcePool.Count; i++)
        {
            if (!enemyResourcePool[i].activeInHierarchy)
            {
                enemyResourcePool[i].transform.position = enemyResourcePool[i].GetComponent<EnemyAttribute>().spawnPoint;
                enemyResourcePool[i].transform.rotation = enemyResourcePool[i].GetComponent<EnemyAttribute>().spawnQuaternion;
                enemyResourcePool[i].GetComponent<EnemyAttribute>().currentHealth = enemyResourcePool[i].GetComponent<EnemyAttribute>().startingHealth;
                enemyResourcePool[i].GetComponent<EnemyAttribute>().isDead = false;
                enemyResourcePool[i].SetActive(true);
            }
        }
    }
    #endregion
}
