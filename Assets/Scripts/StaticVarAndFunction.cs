﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVarAndFunction : MonoBehaviour
{
    public static StaticVarAndFunction instance = null;
    public static bool PlayerIsDead;
    public static bool isLoading;
    //public static GameObject player = GameObject.FindGameObjectWithTag("Player");
    public static GameObject player;
    public static Bag bag;
    public static Inventory inventory;
    public static ArmedEquipment armedEquipment;
    public static bool mainCamRendered;
    public UIController uic;
    //public static Item[] itemlist;

    // Use this for initialization
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void SetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

}
