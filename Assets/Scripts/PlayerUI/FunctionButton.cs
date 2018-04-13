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
        Attribute.SetActive(!Attribute.activeSelf);        
    }

    public void EnableDragDropTest()
    {
        //DragDropTest.SetActive(!DragDropTest.activeSelf);
    }

    public void EnableSetting()
    {
        Setting.SetActive(!Setting.activeSelf);
    }

    public void EnableBag()
    {
        Bag.SetActive(!Bag.activeSelf);
    }

    public void EnableMission()
    {
        Mission.SetActive(!Mission.activeSelf);
    }

    public void EnableSkill() {
        Skill.SetActive(!Skill.activeSelf);
    }

    public void EnableShop()
    {
        Shop.SetActive(!Shop.activeSelf);
        Bag.SetActive(Shop.activeSelf);
    }
    #endregion

}
