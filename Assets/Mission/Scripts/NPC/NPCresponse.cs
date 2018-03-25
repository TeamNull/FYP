using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCresponse : MonoBehaviour {

    GameObject player;
    Scene scene;

    void Start()
    {
        player = StaticVarAndFunction.player;     
        scene = SceneManager.GetSceneAt(1);
    }

    void OnMouseDown()
    {
        //Debug.Log(this.gameObject.transform.name);
        //Debug.Log(scene.name);
        //Debug.Log(player.name);
        player.GetComponent<MissionSystem>().Missiontype2(this.gameObject.transform.name, scene.name);
    }
}
