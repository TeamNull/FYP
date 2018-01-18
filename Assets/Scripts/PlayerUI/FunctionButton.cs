using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionButton : MonoBehaviour
{

    #region Variable
    public GameObject Attribute;
    public GameObject DragDropTest;
    public GameObject Setting;
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
        if (Attribute.activeSelf)
        {
            Attribute.SetActive(false);
        }
        else
        {
            Attribute.SetActive(true);
        }
    }

    public void EnableDragDropTest()
    {
        if (DragDropTest.activeSelf)
        {
            DragDropTest.SetActive(false);
        }
        else
        {
            DragDropTest.SetActive(true);
        }
    }

    public void EnableSetting()
    {
        Setting.SetActive(!Setting.activeSelf);
    }
    #endregion

}
