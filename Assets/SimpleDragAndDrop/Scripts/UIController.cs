using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public int[] LocalSkill = new int[5];
    public Text[] SkillUI = new Text[5];
    public bool SkillUpEnabled;
    public Text ButtonText;
    public Text SkillTitle;
    public int UsedPoint;

    PlayerAttribute pa;

    void Start()
    {
        
    }

    public void SkillUpSwitch()
    {
        if (pa.SkillPoint > 0)
        {
            if (SkillUpEnabled)
            {
                for (int i = 0; i < 5; i++)
                {
                    pa.Skill[i] = LocalSkill[i];
                }
            }
            SkillUpEnabled = !SkillUpEnabled;
            ButtonText.text = SkillUpEnabled ? "Complete" : "Add";
            DragAndDropItem.dragDisabled = SkillUpEnabled;
        }
    }

    public void InitSkillUI()
    {
        pa = GameManager.player.GetComponent<PlayerAttribute>();
        for (int i = 0; i < 5; i++)
        {
            LocalSkill[i] = pa.Skill[i];
            SkillUI[i].text = LocalSkill[i].ToString();
        }
        UpdateTitle(pa.SkillPoint);
        pa.LevelUp += PlayerLevelUp;
        GameManager.Instance.uic = this;
    }

    public void UpdateLocalSkill(int index, bool isAdd)
    {
        if (isAdd)
        {
            if (pa.SkillPoint > UsedPoint)
            {
                if (LocalSkill[index] < 10)
                {
                    bool canAdd = false;
                    switch (index)
                    {
                        case 0:
                            canAdd = true;
                            break;
                        case 1:
                            if (LocalSkill[0] > 0) canAdd = true;
                            break;
                        case 2:
                            if (LocalSkill[0] > 0 && LocalSkill[1] > 0) canAdd = true;
                            break;
                        case 3:
                            if (LocalSkill[0] > 0) canAdd = true;
                            break;
                        case 4:
                            if (LocalSkill[0] > 0 && LocalSkill[3] > 0) canAdd = true;
                            break;
                    }
                    if (canAdd)
                    {
                        LocalSkill[index]++;
                        SkillUI[index].text = LocalSkill[index].ToString();
                        UsedPoint++;
                        UpdateTitle(pa.SkillPoint - UsedPoint);
                    }
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

    void PlayerLevelUp() {
        UpdateTitle(pa.SkillPoint);
    }

    void UpdateTitle(int point)
    {
        SkillTitle.text = "Skill - Available Point: " + point.ToString();
    }
}
