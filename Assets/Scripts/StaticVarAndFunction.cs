using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticVarAndFunction : MonoBehaviour {

    public static bool PlayerIsDead = false;
    public static bool isLoading = false;
    public static GameObject player = GameObject.FindGameObjectWithTag("Player");
    public static Bag bag;
    public static Inventory inventory;
    public static ArmedEquipment armedEquipment;
	//public static Item[] itemlist;

	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
