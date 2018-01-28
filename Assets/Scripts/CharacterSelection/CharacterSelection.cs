using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour {

    private GameObject[] characterObjectList;
    private int index = 0;

    private void Start()
    {
        index = PlayerPrefs.GetInt("CharacterSelected");

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
        if (characterObjectList[index])
        {
            characterObjectList[index].SetActive(true);
        }
    }

    public void ToggleLeft()
    {
        // Toogle off the current model
        characterObjectList[index].SetActive(false);

        index = index - 1;
        if (index < 0)
        {
            index = characterObjectList.Length - 1;
        }

        // Toogle on the current model
        characterObjectList[index].SetActive(true);
    }

    public void ToggleRight()
    {
        // Toogle off the current model
        characterObjectList[index].SetActive(false);

        index = index + 1;
        if (index == characterObjectList.Length)
        {
            index = 0;
        }

        // Toogle on the current model
        characterObjectList[index].SetActive(true);
    }

    public void ConfirmSelection()
    {
        PlayerPrefs.SetInt("CharacterSelected", index);
        SceneManager.LoadScene("VillageTesting");
    }
}
