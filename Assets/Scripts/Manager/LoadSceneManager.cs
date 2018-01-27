using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    GameObject loadingScene;
    Transform playerTransform;

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
                    StartCoroutine(LoadScene("Village", new Vector3(36.9f, 0, 37.12f), new Quaternion(0, 180f, 0, playerTransform.rotation.w)));
                    StartCoroutine(UnloadScene("Forest"));
                    break;
                case "Village":
                    StartCoroutine(LoadScene("Forest", new Vector3(-6.695f, 0, 22.756f), new Quaternion(0, 180f, 0, playerTransform.rotation.w)));
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
        playerTransform.position = v3;
        playerTransform.rotation = q;
    }

    void DeactiveLoadingScene(Scene scene)
    {
        if (loadingScene != null) loadingScene.SetActive(false);
        StaticVarAndFunction.isLoading = false;
    }
}
