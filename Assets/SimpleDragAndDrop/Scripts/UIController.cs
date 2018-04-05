using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public int[] LocalSkill = new int[6];
    public Text[] SkillUI = new Text[6];
    public bool SkillUpEnabled;
    public Text ButtonText;
    public Text SkillTitle;
    public int UsedPoint;

    PlayerAttribute pa;

    void Start()
    {
        //CreateNewCharacter.UILoadComplete += InitSkillUI;
    }

    public void SkillUpSwitch()
    {
        if (pa.SkillPoint > 0)
        {
            if (SkillUpEnabled)
            {
                for (int i = 0; i < 6; i++)
                {
                    pa.Skill[i] = LocalSkill[i];
                }
            }
            SkillUpEnabled = !SkillUpEnabled;
            ButtonText.text = SkillUpEnabled ? "Complete" : "Add";
        }
    }

    public void InitSkillUI()
    {
        pa = StaticVarAndFunction.player.GetComponent<PlayerAttribute>();
        for (int i = 0; i < 6; i++)
        {
            LocalSkill[i] = pa.Skill[i];
            SkillUI[i].text = LocalSkill[i].ToString();
        }
        UpdateTitle(pa.SkillPoint);
    }

    public void UpdateLocalSkill(int index, bool isAdd)
    {
        if (isAdd)
        {
            if (pa.SkillPoint > UsedPoint)
            {
                if (LocalSkill[index] < 10)
                {
                    LocalSkill[index]++;
                    SkillUI[index].text = LocalSkill[index].ToString();
                    UsedPoint++;
                    UpdateTitle(pa.SkillPoint - UsedPoint);
                }
            }
        }
        else
        {
            if (UsedPoint > 0)
            {
                if (LocalSkill[index] > pa.Skill[index])
                {
                    LocalSkill[index]--;
                    SkillUI[index].text = LocalSkill[index].ToString();
                    UsedPoint--;
                    UpdateTitle(pa.SkillPoint - UsedPoint);
                }
            }
        }
    }

    void UpdateTitle(int point)
    {
        SkillTitle.text = "Skill - Available Point: " + point.ToString();
    }
}
