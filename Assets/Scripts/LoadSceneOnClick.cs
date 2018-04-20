using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadSceneOnClick : MonoBehaviour {

    public void LoadByIndex(int sceneIndex)
    {
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("ClickButton");
        SceneManager.LoadScene(sceneIndex);
        Destroy(this.transform.parent.parent.gameObject);
    }

    public void ConfirmSelection()
    {
        StartCoroutine(LoadVillage());
        StartCoroutine(LoadUI());
    }

    IEnumerator LoadUI()
    {
        AsyncOperation loadUI = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        while (!loadUI.isDone)
        {
            yield return null;
        }

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (!Equals(player.name, "Warrior"))
            {
                player.tag = "Untagged";
                player.SetActive(false);
            }
        }
        GameObject[] icons = GameObject.FindGameObjectsWithTag("PlayerIcon");
        foreach (GameObject icon in icons)
        {
            if (!Equals(icon.name, "Warrior"))
            {
                icon.tag = "Untagged";
                icon.SetActive(false);
            }
        }
        GameManager.SetPlayer();
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        ThirdPersonCamera thirdPersonCamera = mainCamera.GetComponent<ThirdPersonCamera>();
        thirdPersonCamera.SetCamPos();
        Minimap miniMapCamera = GameObject.Find("MiniMapCamera").GetComponent<Minimap>();
        miniMapCamera.Init();
        SceneManager.GetSceneByName("UI").GetRootGameObjects()[0].transform.Find("Skill").GetComponent<UIController>().InitSkillUI();
        SaveLoadManager slm = GameObject.FindWithTag("Setting").GetComponent<SaveLoadManager>();
        slm.Init();
        slm.LoadBtnOnClick();
        //slm.gameObject.SetActive(false);
        //StaticVarAndFunction.UnloadNewCharacter();
        GameManager.isLoading = false;
        Destroy(this.transform.parent.parent.gameObject);
    }

    IEnumerator LoadVillage()
    {
        GameManager.isLoading = true;
        AsyncOperation loadVillage = SceneManager.LoadSceneAsync("Village", LoadSceneMode.Single);
        while (!loadVillage.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Village"));
        GameManager.AudioManager.GetComponent<BGMcontrol>().SetBGM("Village");
    }
}
