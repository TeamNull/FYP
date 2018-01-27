using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CityToForest : MonoBehaviour
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
            StartCoroutine(LoadForest());
            StartCoroutine(UnloadVillage());
        }
    }

    IEnumerator UnloadVillage()
    {
        AsyncOperation unloadForest = SceneManager.UnloadSceneAsync("Village");
        while (!unloadForest.isDone)
        {
            yield return null;
        }

    }

    IEnumerator LoadForest()
    {
        loadingScene.SetActive(true);
        AsyncOperation loadVillage = SceneManager.LoadSceneAsync("Forest", LoadSceneMode.Additive);
        while (!loadVillage.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Forest"));
        playerTransform.position = new Vector3(-6.695f, 0, 22.756f);
        playerTransform.rotation.Set(0, 180f, 0, playerTransform.rotation.w);
        loadingScene.SetActive(false);
    }
}
