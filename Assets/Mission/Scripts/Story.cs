using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Story : MonoBehaviour
{

    public Scene UI;
    public GameObject playerUI;
    public GameObject storyBoard;
    public Text storytext;
    public Button nextline;
    private int counter = 0;

    void Start()
    {
      
    }

    public void Callstory(int mission)
    {
        StartCoroutine(Loadstory(mission));
    }

    IEnumerator Loadstory(int mission)
    {
        playerUI.SetActive(false);
        storyBoard.SetActive(true);

        if (mission == 0)
        {
            storytext.text = "Where should I go?";

            yield return new WaitForSeconds(3);

            storytext.text = "Oh, maybe I should go to the church and ask the father.";

            yield return new WaitForSeconds(3);
        }
       

        storyBoard.SetActive(false);
        playerUI.SetActive(true);

      
    }
}
