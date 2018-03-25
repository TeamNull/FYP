using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionSystem : MonoBehaviour {

    private int globalMissionID;
    private int enemycount = 0 ;

    // Use this for initialization
    void Start () {
        Setmission();
        MissionStart(0);
    }

    public Text titletext; // mission number of the mission
    public Text descriptiontext; // the text show on the mission canves
    public Text requirementtext;
    public Text progresstext;


    public const int missionnumber = 24; // totoal mission number 


    Mission[] mission  = new Mission[missionnumber];
    void Setmission()
    {  
        
        //mission[0] = new MissionTypeLocation(0, 0, " description1", "Go to forest 0,0,0", "location0","Forest", false);
       
        //mission[1] = new MissionTypeLocation(1, 0, "description2", "Go to forest 10,1,0", "location1", "Forest", false);

        mission[0] = new MissionTypeEnemy(0, 1, "description3", "Kill 3 spiders", "spider(Clone)", 3, "Forest", false);

        return;

    }

    void MissionStart(int missionID)
    {
        globalMissionID = missionID;
        titletext.text = "Mission " + (missionID + 1).ToString();
        descriptiontext.text = mission[missionID].Getdescription();
        requirementtext.text = mission[missionID].Getrequirement();
        //Debug.Log("this is global" + globalMissionID);
        //Debug.Log("this is missionID" + mission[missionID].GetmissionID());
 /*       if (mission[missionID].Gettype() == 0)
        {

            descriptiontext.text = mission[missionID].Getdescription();

            //GameObject[] uiGameObjectArray = SceneManager.GetSceneByName(mission[missionID].Getscene()).GetRootGameObjects();
            //foreach (GameObject target in uiGameObjectArray)
            //{
            //    if (target.name == mission[missionID].Getrectname())
            //    {
            //        target.SetActive(true);
            //    }
            //}
            return;
        }

 */   
        if (mission[missionID].Gettype() == 1)
        {
            progresstext.text = enemycount.ToString() + " out of " + mission[globalMissionID].Getcountcdienum().ToString();
            return;
        }

        return;
 
    }


    void OnTriggerEnter(Collider other)
    {
        
        if (other.name == mission[globalMissionID].Getrectname() && mission[globalMissionID].Gettype() == 0)
        {
            //Debug.Log("1");
            MissionComplete(globalMissionID);
            return;
        }

        //if (other.name == mission[globalMissionID].Getenemyname() && mission[globalMissionID].Gettype() == 1)
        //{
        //    enemycount = 0;
        //    if (!other.gameObject.activeSelf)
        //    {
        //        //enemycount++;
        //        progresstext.text = enemycount.ToString() + " out of " + mission[globalMissionID].Getcountcdienum().ToString();
        //        Debug.Log(enemycount);
        //    }

        //    if (enemycount == mission[globalMissionID].Getcountcdienum())
        //        progresstext.text = " ";
        //        //Debug.Log("2");
        //        MissionComplete(globalMissionID);

        //    return;
        //}

    }

    public void Missiontype1counter(int count, string mountername)
    {
        if (mission[globalMissionID].Gettype() == 1)
        {
            if (mission[globalMissionID].Getenemyname() == mountername)
            {
                enemycount = enemycount + count;
                Missiontype1main();
                Debug.Log("this is " + enemycount);

            }
        }

    }

    public void Missiontype1main()
    {
        progresstext.text = enemycount.ToString() + " out of " + mission[globalMissionID].Getcountcdienum().ToString();

        if (enemycount == mission[globalMissionID].Getcountcdienum())
        {
            enemycount = 0;
            progresstext.text = " ";
            MissionComplete(globalMissionID);
        }

    }




void MissionComplete(int missionID)
    {
        
        mission[missionID].Setcomplete(true);

        if (mission[missionID+1] != null)
        {

            missionID++;
            MissionStart(missionID);
            
            return;
        }


        if (mission[missionID + 1] == null)
        {
            descriptiontext.text = "Mission complete";
            requirementtext.text = "";
            return;
        }
    }

}
