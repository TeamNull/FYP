using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIinfo : MonoBehaviour
{

    public Slider hpSlider;
    public Slider mpSlider;
    public Slider expSlider;

    public Text currentLevelText;
    public Text currentExpText;
    public Text levelUpText;
    public Text hpText;
    public Text mpText;

    Animator expAnim;

    // Use this for initialization
    void Start()
    {
        expAnim = GameObject.FindGameObjectWithTag("Exp").GetComponent<Animator>();
        //StartCoroutine(UnloadNewCharacter());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void updateHP(int current, int total)
    {
        hpSlider.value = (int)Mathf.Floor((100 * current / total));
        hpText.text = current + " / " + total;        
    }

    public void updateMP(int current, int total)
    {
        mpSlider.value = (int)Mathf.Floor((100 * current / total));
        mpText.text = current + " / " + total;        
    }

    public void updateEXP(int curLv, int current, int total, bool isLvUp)
    {
        if (isLvUp) expAnim.SetTrigger("LevelUp");//ui anim        
        currentLevelText.text = "LV " + curLv + " ( " + (100 * current / total) + "% )";
        expSlider.value = (int)Mathf.Floor((100 * current / total));
        currentExpText.text = current + " / " + total ;        
    }

    //will be used for allow player to customize shortsut lately
    public void updateShortCut()
    {

    }

    IEnumerator UnloadNewCharacter()
    {
        AsyncOperation unloadNewCharacter = SceneManager.UnloadSceneAsync("NewCharacter");
        while (!unloadNewCharacter.isDone)
        {
            yield return null;
        }
        GameManager.isLoading = false;
    }

}