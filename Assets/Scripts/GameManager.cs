using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static bool PlayerIsDead;
    public static bool isLoading;
    //public static GameObject player = GameObject.FindGameObjectWithTag("Player");
    public static GameObject player;
    public static Bag bag;
    public static Inventory inventory;
    public static ArmedEquipment armedEquipment;
    public static bool mainCamRendered;
    public static InGameLog inGameLog;
    public UIController uic;
    //public static Item[] itemlist;

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("Game").AddComponent<GameManager>();
            }
            return instance;
        }
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
