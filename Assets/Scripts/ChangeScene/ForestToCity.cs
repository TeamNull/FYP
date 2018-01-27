using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForestToCity : MonoBehaviour
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

            if (go.name == "unitychan")
            {
                playerTransform = go.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(LoadVillage());
            StartCoroutine(UnloadForest());
        }
    }

    IEnumerator UnloadForest()
    {
        AsyncOperation unloadForest = SceneManager.UnloadSceneAsync("Forest");
        while (!unloadForest.isDone)
        {
            yield return null;
        }

    }

    IEnumerator LoadVillage()
    {
        loadingScene.SetActive(true);
        AsyncOperation loadVillage = SceneManager.LoadSceneAsync("Village", LoadSceneMode.Additive);
        while (!loadVillage.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Village"));
        playerTransform.position = new Vector3(36.9f, 0, 37.12f);
        playerTransform.rotation.Set(0, 180f, 0, playerTransform.rotation.w);
        loadingScene.SetActive(false);
    }
}
