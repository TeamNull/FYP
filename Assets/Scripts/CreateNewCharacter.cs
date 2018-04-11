using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CreateNewCharacter : MonoBehaviour
{
    static public string nameOfPlayer;
    static public int numOfSelectedCharacter; //0 warriorBoy, 1 archerGirl, 2 magic
    public Text temp;
    private GameObject[] characterObjectList;

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        numOfSelectedCharacter = PlayerPrefs.GetInt("CharacterSelected");

        characterObjectList = new GameObject[transform.childCount];

        // Fill the characterobjectlist array with models
        for (int i = 0; i < transform.childCount; i++)
        {
            characterObjectList[i] = transform.GetChild(i).gameObject;
        }

        // Toogle off renderer for all elements in the list
        foreach (GameObject go in characterObjectList)
        {
            go.SetActive(false);
        }

        // Toogle on current character
        if (characterObjectList[numOfSelectedCharacter])
        {
            characterObjectList[numOfSelectedCharacter].SetActive(true);
        }
    }

    public void ToggleLeft()
    {
        Debug.Log("confirmselection");
        // Toogle off the current model
        characterObjectList[numOfSelectedCharacter].SetActive(false);

        numOfSelectedCharacter = numOfSelectedCharacter - 1;
        if (numOfSelectedCharacter < 0)
        {
            numOfSelectedCharacter = characterObjectList.Length - 1;
        }

        // Toogle on the current model
        characterObjectList[numOfSelectedCharacter].SetActive(true);
    }

    public void ToggleRight()
    {
        // Toogle off the current model
        characterObjectList[numOfSelectedCharacter].SetActive(false);

        numOfSelectedCharacter = numOfSelectedCharacter + 1;
        if (numOfSelectedCharacter == characterObjectList.Length)
        {
            numOfSelectedCharacter = 0;
        }

        // Toogle on the current model
        characterObjectList[numOfSelectedCharacter].SetActive(true);
    }

    public void ConfirmSelection()
    {
        nameOfPlayer = temp.text;
        GameManager.isLoading = true;
        StartCoroutine(LoadVillage());
        StartCoroutine(LoadUI());
        //StartCoroutine(UnloadNewCharacter());
        //StaticVarAndFunction.helpUnloadNewCharacter();
    }

    IEnumerator LoadUI()
    {
        AsyncOperation loadUI = SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        while (!loadUI.isDone)
        {
            yield return null;
        }
        string tempJob = "";
        switch (numOfSelectedCharacter)
        {
            case 0:
                tempJob = "Warrior";
                break;
            case 1:
                tempJob = "Archer";
                break;
            case 2:
                tempJob = "Magician";
                break;
        }
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (!Equals(player.name, tempJob))
            {
                player.tag = "Untagged";
                player.SetActive(false);
            }
        }
        GameObject[] icons = GameObject.FindGameObjectsWithTag("PlayerIcon");
        foreach (GameObject icon in icons)
        {
            if (!Equals(icon.name, tempJob))
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
        //StaticVarAndFunction.UnloadNewCharacter();
        GameManager.isLoading = false;
        Destroy(this.gameObject);
    }

    IEnumerator LoadVillage()
    {
        AsyncOperation loadVillage = SceneManager.LoadSceneAsync("Village", LoadSceneMode.Single);
        while (!loadVillage.isDone)
        {
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Village"));
    }
}
