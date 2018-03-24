using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    GameObject loadingScene;
    Transform playerTransform;
    Transform BeginPosition;

    // Use this for initialization
    void Start()
    {
        GameObject[] uiGameObjectArray = SceneManager.GetSceneByName("UI").GetRootGameObjects();
        foreach (GameObject go in uiGameObjectArray)
        {
            if (go.name == "PlayerUI")
            {
                loadingScene = go.transform.Find("Loading Scene").gameObject;
            }
        }
        playerTransform = StaticVarAndFunction.player.transform;
        SceneManager.sceneUnloaded += DeactiveLoadingScene;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (SceneManager.GetActiveScene().name)
            {
                case "Forest":
                    StartCoroutine(LoadScene("Village", new Vector3(37.116f, 0.1399994f, 37.678f), new Quaternion(0, 180f, 0, playerTransform.rotation.w)));
                    StartCoroutine(UnloadScene("Forest"));
                    break;
                case "Village":
                    StartCoroutine(LoadScene("Forest", new Vector3(57.69f, 0.1399994f, -2.81f), new Quaternion(0, 0f, 0, playerTransform.rotation.w)));
                    StartCoroutine(UnloadScene("Village"));
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator UnloadScene(string sceneName)
    {
        AsyncOperation unloadForest = SceneManager.UnloadSceneAsync(sceneName);
        while (!unloadForest.isDone)
        {
            yield return null;
        }

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
            SceneManager.GetActiveScene().GetRootGameObjects()[2].GetComponent<EnemyManager>().Init();
        }
        playerTransform.position = v3;
        playerTransform.rotation = q;
    }

    void DeactiveLoadingScene(Scene scene)
    {
        if (loadingScene != null) loadingScene.SetActive(false);
        StaticVarAndFunction.isLoading = false;
    }
}
