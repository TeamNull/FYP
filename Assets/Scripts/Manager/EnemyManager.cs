using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public Transform[] SpawnPoint;
    public GameObject enemy;
    public int respwanTime = 5;

	// Use this for initialization
	void Start () {
        foreach(Transform t in SpawnPoint) {
            Instantiate(enemy, t.position, t.rotation);
        }
    }
	
	// Update is called once per frame
    void Update () {
        
    }

    public void Spawn(Vector3 v3, Quaternion q) {
        StartCoroutine(Wait(v3, q));
    }

    IEnumerator Wait(Vector3 v3, Quaternion q)
    {
        yield return new WaitForSeconds(respwanTime);
        Instantiate(enemy, v3, q);
    }
}
