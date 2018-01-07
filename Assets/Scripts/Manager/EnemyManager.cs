using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public Transform[] SpawnPoint;
    public GameObject enemy;

	// Use this for initialization
	void Start () {
        foreach(Transform t in SpawnPoint) {
            Spawn(t);
        }
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void Spawn(Transform t) {
        Instantiate(enemy, t.position, t.rotation);
    }
}
