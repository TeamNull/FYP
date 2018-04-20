using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillClick : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    , IPointerExitHandler
{
    public GameObject popUp;
    Item item;
    UIController uic;

    void Awake()
    {
        uic = GameManager.Instance.uic;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (uic.SkillUpEnabled)
            {
                uic.UpdateLocalSkill(int.Parse(name), true);
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (uic.SkillUpEnabled)
            {
                uic.UpdateLocalSkill(int.Parse(name), false);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        popUp = GameObject.FindGameObjectWithTag("PopUp");
        popUp.transform.position += (new Vector3(80, -40, 0));
        item = transform.GetComponent<tempSkillItem>().skillItem;
        if (item != null)
        {
            popUp = GameObject.FindGameObjectWithTag("PopUp");
            //popUp.GetComponentInChildren<Text>().text = item.description;
            popUp.transform.GetChild(0).GetComponent<Text>().text = item.itemName;
            popUp.transform.GetChild(2).GetComponent<Text>().text = item.description;
            popUp.transform.GetChild(3).transform.gameObject.SetActive(false);
            popUp.transform.GetChild(4).transform.gameObject.SetActive(false);
            popUp.transform.position = this.transform.position;
            popUp.transform.position += (new Vector3(100, -20, 0));
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popUp = GameObject.FindGameObjectWithTag("PopUp");
        popUp.transform.GetChild(3).transform.gameObject.SetActive(true);
        popUp.transform.GetChild(4).transform.gameObject.SetActive(true);
        popUp.transform.position += (new Vector3(10000, 10000, 0));
    }
}
