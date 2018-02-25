using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticVarAndFunction : MonoBehaviour {

    public static bool PlayerIsDead = false;
    public static bool isLoading = false;
    public static GameObject player = GameObject.FindGameObjectWithTag("Player");
    public static Inventory bag;
	//public static Item[] itemlist;

	// Use this for initialization
	void Start () {
        GameObject[] uiGameObjectArray = SceneManager.GetSceneByName("UI").GetRootGameObjects();
        foreach (GameObject go in uiGameObjectArray)
        {
            if (go.name == "PlayerUI")
            {
                bag = go.transform.Find("Bag").GetChild(2).GetComponent<Inventory>();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

}
