using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCresponse : MonoBehaviour {

    GameObject player;
    Scene scene;
    Transform playerposition;
    Transform thisposition;
    float dist;

    void Start()
    {
        player = GameManager.player;     
    }


    void OnMouseDown()
    {
        //Debug.Log(this.gameObject.transform.name);
        //Debug.Log(scene.name);
        if(player == null)
        {
            player = GameManager.player;
        }

        dist = Vector3.Distance(player.transform.position, this.transform.position);

        if(dist < 3)
        {   
            player.GetComponent<MissionSystem>().Missiontype1handling(this.gameObject.transform.name);
            player.GetComponent<MissionSystem>().Missiontype2(this.gameObject.transform.name);
            player.GetComponent<MissionSystem>().Missiontype3handling();
        }
       
    }
}
