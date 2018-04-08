using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillClick : MonoBehaviour, IPointerClickHandler {

    UIController uic;

    void Awake()
    {
        uic = StaticVarAndFunction.instance.uic;
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
}
