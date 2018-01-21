using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVarAndFunction : MonoBehaviour {

    public static bool PlayerIsDead = false;
    public static bool isLoading = false;
    public static GameObject player = GameObject.FindGameObjectWithTag("Player");

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
