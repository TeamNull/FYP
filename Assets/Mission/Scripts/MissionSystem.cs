using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionSystem : MonoBehaviour
{

    private int globalMissionID = 0;
    private int enemycount = 0;
    Mission[] mission = new Mission[missionnumber];
    GameObject player;
    private bool missiontype1enemy = false;
    private bool missiontype3item = false;
    private bool vulture = false;
    private bool vultureBoss = false;
    private bool vulturecreated = false;
    private bool vultureBossecreate = false;

    GameObject loadingScene;
    GameObject[] ForestGameObjectArray = SceneManager.GetSceneByName("Forest").GetRootGameObjects();
    GameObject[] RuinsGameObjectArray = SceneManager.GetSceneByName("Ruins").GetRootGameObjects();

    // Use this for initialization
    void Start()
    {
        Setmission();
        MissionStart(globalMissionID);
        player = StaticVarAndFunction.player;

        GameObject[] uiGameObjectArray = SceneManager.GetSceneByName("UI").GetRootGameObjects();
        
       foreach (GameObject go in uiGameObjectArray)    
       {
           if (go.name == "PlayerUI")            
           {  
               loadingScene = go.transform.Find("Loading Scene").gameObject;       
           }           
       }
    }
    public GameObject vulture1;
    public Scene forest;
    public Scene ruins;
    public Text titletext; // mission number of the mission
    public Text descriptiontext; // the text show on the mission canves
    public Text requirementtext;
    public Text progresstext;


    public const int missionnumber = 24; // totoal mission number 


    void ChangeScene(Vector3 v, Quaternion q, string to, string from)	
   {	
       StartCoroutine(LoadScene(to, v, q));	
       StartCoroutine(UnloadScene(from));
   }
   IEnumerator UnloadScene(string sceneName)
   {
       AsyncOperation unloadForest = SceneManager.UnloadSceneAsync(sceneName);
       while (!unloadForest.isDone)
       {
           yield return null;
       }	
       loadingScene.SetActive(false);
       StaticVarAndFunction.isLoading = false;	
   }

   IEnumerator LoadScene(string sceneName, Vector3 v3, Quaternion q)
   {
       loadingScene.SetActive(true);	
       StaticVarAndFunction.isLoading = true;	
       AsyncOperation loadVillage = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);	
       while (!loadVillage.isDone)
       {
           yield return null;
       }
       SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));	
       if (sceneName == "Forest")	
       {	
           SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0].GetComponent<EnemyManager>().Init();	
           SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[1].GetComponent<EnemyManager>().Init();	
           SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[2].GetComponent<EnemyManager>().Init();	
           SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[3].GetComponent<EnemyManager>().Init(); //boss
           SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0].GetComponent<Weather>().SwitchSkyBox(3);
       }	
       if (sceneName == "Ruins")	
       {
           SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0].GetComponent<EnemyManager>().Init();
           SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[1].GetComponent<EnemyManager>().Init(); //boss

       }	
       player.transform.position = v3;	
       player.transform.rotation = q;	
   }


    void SetglobalMissionID(int missionID)
    {
        globalMissionID = missionID;
    }

    void Setmission()
    {

        //testing use

        //mission[0] = new MissionTypeLocation(0, 0, " description1", "Go to forest 0,0,0", "location0","Forest", false);

        //mission[1] = new MissionTypeLocation(1, 0, "description2", "Go to forest 10,1,0", "location1", "Forest", false);

        //mission[2] = new MissionTypeEnemy(2, 1, "description3", "Kill 3 spiders", "spider(Clone)", 3, "Forest", false);

        //mission[3] = new MissionTypeNPC(3, 2, " description4", "find Commander", "Commander", "FrontlineBase", false);

        //mission[4] = new MissionTypeItem(0, 3, " description5", "get Katana", 100, "Village", false);

        //real

        mission[0] = new MissionTypeLocation(0, 0, "Go to village", "Find a path to village", "Mission1", "Villiage", false); //37.37792,03999996,38.11484 

        mission[1] = new MissionTypeLocation(1, 0, "Go to church", "Find a path to church", "Mission2", "Villiage", false);

        mission[2] = new MissionTypeNPC(2, 2, "Find the Chief of the warrior", "Find a path to Chief of the warrior", "Chief of the warrior", "Villiage", false);

        mission[3] = new MissionTypeEnemy(3, 1, "Go to forest and kill 3 Spiders", "Kill 3 Spiders and report to the Chief of the warrior", "Minotaur(Clone)", 3, "Chief of the warrior", "Forest", false);

        mission[4] = new MissionTypeItem(4, 3, "Collect item 1", "Go to the village and pick up item 1", 100, "Village", false); // wait for the item list complete

        mission[5] = new MissionTypeLocation(5, 0, "Find out the Unknown!", "Go to the forest and kill the unknown", "Mission5", "Forest", false); // auto generate unknows and close the portal

        mission[6] = new MissionTypeLocation(6, 0, "Make a new front map", "Visit the Ruins and explore the new map", "Mission6", "Ruins", false);

        mission[7] = new MissionTypeNPC(7, 2, "Find the Chief of the army", "Find the location of Chief of army", "Chief of the army", "Villiage", false);

        mission[8] = new MissionTypeNPC(8, 2, "Find the Chief of the warrior", "Find a path to Chief of the warrior", "Chief of the warrior", "Villiage", false);

        mission[9] = new MissionTypeLocation(9, 0, "Collect intelligence about unknown", "Predict the number of unknown at front line and report to Chief of the Warrior", "Mission9", "Ruins", false);

        mission[10] = new MissionTypeItem(10, 3, "Collect item 2", "Go to the forest and pick up item 2", 100, "Forest", false); // wait for the item list complete

        mission[11] = new MissionTypeNPC(11, 2, "Meet the Commander", "Go to Frontline base and meet the commander", "Commander", "FrontlineBase", false);

        mission[12] = new MissionTypeItem(12, 3, "Find the lost material", "Go to ruins and find the lost material ", 100, "Ruins", false); // wait for the item list complete

        mission[13] = new MissionTypeNPC(13, 2, "Find the Chief of the warrior", "Back to the village and find the Chief of the warrior", "Chief of the warrior", "Villiage", false);

        mission[14] = new MissionTypeNPC(14, 2, "Find the father", "Find the father", "Father", "Villiage", false);

        mission[15] = new MissionTypeNPC(15, 2, "Return to frontline base", "Go to frontline base and find the commander", "Commander", "FrontlineBase", false);

        mission[16] = new MissionTypeItem(16, 3, "Collect material in the Ruins", "Go to Ruins and find out the material", 100, "Ruins", false); // wait for the item list complete

        mission[17] = new MissionTypeLocation(17, 0, "Check here", "walk and check all the detail", "Mission17", "Ruins", false);

        mission[18] = new MissionTypeLocation(18, 0, "Skirmish!", "Kill them ALL!", "Mission18", "Ruins", false); // auto generate unknows and close the portal

        mission[19] = new MissionTypeNPC(19, 2, "Return to frontline base", "Go to frontline base and find the commander", "Commander", "FrontlineBase", false);

        //real end

        return;

    }

    void MissionStart(int missionID)
    {
        globalMissionID = missionID;

        titletext.text = "Mission " + (missionID + 1).ToString();

        descriptiontext.text = mission[missionID].Getdescription();

        requirementtext.text = mission[missionID].Getrequirement();

        if (mission[missionID].Gettype() == 1)
        {
            progresstext.text = enemycount.ToString() + " out of " + mission[globalMissionID].Getcountcdienum().ToString();
            return;
        }

        return;

    }

    public void Thebossmonsterisdead(string monstername)
    {
        if (monstername == "Vulture")
        {
            vulture = true;
            //teleport function(x , y , scene); 
            MissionComplete(globalMissionID);
        }

        if (monstername == "VultureBoss")
        {
            vultureBoss = true;

            MissionComplete(globalMissionID);
        }
    }

    void Createmonster(string monstername)
    {
        if (monstername == "Vulture" && !vulturecreated)
        {
            foreach (GameObject go in ForestGameObjectArray)
            {
                if (go.name == monstername)
                {
                    go.SetActive(true);
                    vulturecreated = true;
                }
            }
        }

        if (monstername == "vultureBosse" && !vultureBossecreate)
        {
            foreach (GameObject go in RuinsGameObjectArray)
            {
                if (go.name == monstername)
                {
                    go.SetActive(true);
                    vultureBossecreate = true;
                }
            }
        }
    }

    public void Missiontype0handling(string npcname)
    {

        if (mission[globalMissionID].Gettype() == 0 && globalMissionID == 6)
        {
            MissionComplete(globalMissionID);
        }

    }
    void OnTriggerEnter(Collider other) // missiontype0
    {

        if (other.name == mission[globalMissionID].Getrectname() && mission[globalMissionID].Gettype() == 0 && globalMissionID != 6)
        {
            if (globalMissionID == 5)
            {
                Createmonster("Vulture");
            }

            if (globalMissionID == 18)
            {
                Createmonster("VultureBoss");
            }

            MissionComplete(globalMissionID);
            return;
        }

    }

    public void Missiontype1(int count, string mountername)
    {
        if (mission[globalMissionID].Gettype() == 1)
        {
            if (mission[globalMissionID].Getenemyname() == mountername)
            {
                enemycount = enemycount + count;

                progresstext.text = enemycount.ToString() + " out of " + mission[globalMissionID].Getcountcdienum().ToString();

                if (enemycount == mission[globalMissionID].Getcountcdienum())
                {
                    missiontype1enemy = true;
                    Debug.Log(missiontype1enemy);
                    enemycount = 0;
                    progresstext.text = "completed";
                }


            }
        }

    }

    public void Missiontype1handling(string npcname)
    {

        if (missiontype1enemy && mission[globalMissionID].Gettype() == 1)
        {
            if (mission[globalMissionID].Getreportnpc() == npcname)
            {
                missiontype1enemy = false;
                MissionComplete(globalMissionID);
            }
        }

    }


    public void Missiontype2(string npcname)
    {
        if (mission[globalMissionID].Gettype() == 2)
        {
            if (mission[globalMissionID].Getnpc() == npcname)
            {
                MissionComplete(globalMissionID);
            }
        }

    }

    public void Missiontype3(int iteamid)
    {
        if (mission[globalMissionID].Gettype() == 3)
        {
            if (mission[globalMissionID].Getitemid() == iteamid)
            {
                progresstext.text = "completed";
                missiontype3item = true;
                Debug.Log("missiontype3");
            }
        }

    }

    public void Missiontype3handling()
    {
        if (missiontype3item && mission[globalMissionID].Gettype() == 3)
        {
            missiontype3item = false;
            Debug.Log("missiontype3handling");
            MissionComplete(globalMissionID);
        }
    }

    void MissionComplete(int missionID)
    {

        mission[missionID].Setcomplete(true);

        if (mission[missionID + 1] != null)
        {
            if (player == null)
            {
                player = StaticVarAndFunction.player;
            }
            if (globalMissionID != 5 && globalMissionID != 6 && globalMissionID != 9 && globalMissionID != 18)
            {
                player.GetComponent<Story>().Callstory(globalMissionID);
            }
            progresstext.text = "";
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
