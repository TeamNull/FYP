using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public Transform[] SpawnPoint;
    public GameObject enemy;

    Dictionary<Vector3, GameObject> enemyExistMap;

	// Use this for initialization
	void Start () {
        foreach(Transform t in SpawnPoint) {
            Spawn(t);
        }
    }
	
	// Update is called once per frame
	void Update () {
        foreach(KeyValuePair<Vector3, GameObject> temp in enemyExistMap) {
            print(temp.Value);
        }
	}

    void Spawn(Transform t) {
        GameObject SpawnedEnemy = Instantiate(enemy, t.position, t.rotation);
        enemyExistMap.Add(t.position, SpawnedEnemy);
    }
}
