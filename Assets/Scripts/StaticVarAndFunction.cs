using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StaticVarAndFunction : MonoBehaviour {

    public static bool PlayerIsDead;
    public static bool isLoading;
    //public static GameObject player = GameObject.FindGameObjectWithTag("Player");
    public static GameObject player;
    public static Bag bag;
    public static Inventory inventory;
    public static ArmedEquipment armedEquipment;
    public static bool mainCamRendered;
    //public static Item[] itemlist;

    // Use this for initialization
    void Awake() {

    }

    // Update is called once per frame
    void Update() {

    }

    public static void SetPlayer() {
        player = GameObject.FindGameObjectWithTag("Player");
    }



    
}
