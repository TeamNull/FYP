using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionButton : MonoBehaviour
{

    #region Variable
    public GameObject Attribute;
    //public GameObject DragDropTest;
    public GameObject Setting;
    public GameObject Bag;
    public GameObject Mission;
    public GameObject Skill;
    public GameObject Shop;
    #endregion

    #region LifeCycle
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Method
    public void EnableAttribute()
    {
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("ClickButton");
        Attribute.SetActive(!Attribute.activeSelf);        
    }

    public void EnableDragDropTest()
    {
        //DragDropTest.SetActive(!DragDropTest.activeSelf);
    }

    public void EnableSetting()
    {
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("ClickButton");
        Setting.SetActive(!Setting.activeSelf);
    }

    public void EnableBag()
    {
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("ClickButton");
        Bag.SetActive(!Bag.activeSelf);
    }

    public void EnableMission()
    {
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("ClickButton");
        Mission.SetActive(!Mission.activeSelf);
    }

    public void EnableSkill() {
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("ClickButton");
        Skill.SetActive(!Skill.activeSelf);
    }

    public void EnableShop()
    {
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("ClickButton");
        Shop.SetActive(!Shop.activeSelf);
        Bag.SetActive(Shop.activeSelf);
    }
    #endregion

}
