using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    public string from;
    public string to;

    GameObject loadingScene;
    Transform playerTransform;
    bool initialized;

    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Init() {
        GameObject[] uiGameObjectArray = SceneManager.GetSceneByName("UI").GetRootGameObjects();
        foreach (GameObject go in uiGameObjectArray)
        {
            if (go.name == "PlayerUI")
            {
                loadingScene = go.transform.Find("Loading Scene").gameObject;
            }
        }
        playerTransform = GameManager.player.transform;
        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (!initialized) Init();
        if (other.gameObject.tag == "Player")
        {
            Vector3 destinationPosition = new Vector3();
            Quaternion destinationQuaternion = new Quaternion();
            switch (to)
            {
                case "Forest":
                    if (from == "Village")
                    {
                        destinationPosition = new Vector3(57.69f, 0.1399994f, -2.81f);
                        destinationQuaternion = new Quaternion(0, 0f, 0, playerTransform.rotation.w);
                    }
                    else if (from == "FrontlineBase")
                    {
                        destinationPosition = new Vector3(36.37389f, 0.1399994f, 88.26687f);
                        destinationQuaternion = new Quaternion(0, 180f, 0, playerTransform.rotation.w);
                    }
                    break;
                case "Village":
                    destinationPosition = new Vector3(37.116f, 0.1399994f, 37.678f);
                    destinationQuaternion = new Quaternion(0, 180f, 0, playerTransform.rotation.w);
                    break;
                case "FrontlineBase":
                    if (from == "Forest") {
                        destinationPosition = new Vector3(-12.58f, 0.03999966f, 6.5f);
                        destinationQuaternion = new Quaternion(0, 180, 0, playerTransform.rotation.w);
                    } else if (from == "Ruins") {
                        destinationPosition = new Vector3(1.916f, 0f, 0f);
                        destinationQuaternion = new Quaternion(0, 0, 0, playerTransform.rotation.w);
                    }
                    break;
                case "Ruins":
                    destinationPosition = new Vector3(33.34057f, 5.034346f, 6.439279f);
                    destinationQuaternion = new Quaternion(0, 0, 0, playerTransform.rotation.w);
                    break;
                default:
                    destinationPosition = new Vector3(0, 0, 0);
                    destinationQuaternion = new Quaternion(0, 0, 0, playerTransform.rotation.w);
                    break;
            }
            StartCoroutine(LoadScene(to, destinationPosition, destinationQuaternion));
            StartCoroutine(UnloadScene(from));
        }
    }

    IEnumerator UnloadScene(string sceneName)
    {
        GameObject[] goArray = GameObject.FindGameObjectsWithTag("Portal");

        AsyncOperation unloadForest = SceneManager.UnloadSceneAsync(sceneName);
        while (!unloadForest.isDone)
        {
            yield return null;
        }
        loadingScene.SetActive(false);
        GameManager.isLoading = false;
        foreach(GameObject go in goArray) {
            if (go.GetComponent<LoadSceneManager>().from == sceneName) {
                Destroy(go);
            }
        }
    }

    IEnumerator LoadScene(string sceneName, Vector3 v3, Quaternion q)
    {
        loadingScene.SetActive(true);
        Slider progressBar = loadingScene.GetComponentInChildren<Slider>();
        GameManager.isLoading = true;
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
            //SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[3].GetComponent<EnemyManager>().Init(); //boss
            SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0].GetComponent<Weather>().SwitchSkyBox(3);
        }
        if (sceneName == "Ruins")
        {
            SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[0].GetComponent<EnemyManager>().Init();
            //SceneManager.GetSceneByName(sceneName).GetRootGameObjects()[1].GetComponent<EnemyManager>().Init(); //boss
        }
        playerTransform.position = v3;
        playerTransform.rotation = q;
    }
}
