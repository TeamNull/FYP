using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public int[] LocalSkill = new int[5];
    public Text[] SkillUI = new Text[5];
    public GameObject[] ShortcutItem = new GameObject[3];
    public GameObject[] DadItem = new GameObject[5];
    public bool SkillUpEnabled;
    public Text ButtonText;
    public Text SkillTitle;
    public int UsedPoint;
    public Attribute attibute;

    PlayerAttribute pa;

    void Start()
    {
        
    }

    public void SkillUpSwitch()
    {
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("ClickButton");
        if (pa.SkillPoint > 0)
        {
            if (SkillUpEnabled)
            {
                for (int i = 0; i < 5; i++)
                {
                    pa.Skill[i] = LocalSkill[i];
                }
                pa.SkillPoint -= UsedPoint;
                UsedPoint = 0;
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
                        int job = 0;
                        switch (pa.job)
                        {
                            case PlayerAttribute.Classes.Archer:
                                job = 0;
                                break;
                            case PlayerAttribute.Classes.Magician:
                                job = 1;
                                break;
                            case PlayerAttribute.Classes.Warrior:
                                job = 2;
                                break;
                        }
                        if (index == 0 || index == 1 || index == 2)
                            DadItem[index].GetComponent<DragAndDropItem>().SkillItem[job].skillLevel++;
                        UpdateTitle(pa.SkillPoint - UsedPoint);
                        if (index == 3) {
                            switch (pa.job)
                            {
                                case PlayerAttribute.Classes.Archer:
                                    pa.additionalAtk += 2;
                                    break;
                                case PlayerAttribute.Classes.Magician:
                                    pa.additionalAtk += 2;
                                    break;
                                case PlayerAttribute.Classes.Warrior:
                                    pa.additionalHP += 10;
                                    break;
                            }
                            pa.UpdatePlayerValueByPoint();
                        } else if (index == 4) {
                            switch (pa.job)
                            {
                                case PlayerAttribute.Classes.Archer:
                                    pa.additionalDef += 2;
                                    break;
                                case PlayerAttribute.Classes.Magician:
                                    pa.additionalMP += 10;
                                    break;
                                case PlayerAttribute.Classes.Warrior:
                                    pa.additionalDef += 2;
                                    break;
                            }
                            pa.UpdatePlayerValueByPoint();
                            
                        }
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
                    int job = 0;
                    switch (pa.job)
                    {
                        case PlayerAttribute.Classes.Archer:
                            job = 0;
                            break;
                        case PlayerAttribute.Classes.Magician:
                            job = 1;
                            break;
                        case PlayerAttribute.Classes.Warrior:
                            job = 2;
                            break;
                    }
                    DadItem[index].GetComponent<DragAndDropItem>().SkillItem[job].skillLevel--;
                    UpdateTitle(pa.SkillPoint - UsedPoint);
                    if (index == 3)
                    {
                        switch (pa.job)
                        {
                            case PlayerAttribute.Classes.Archer:
                                pa.additionalAtk -= 2;
                                break;
                            case PlayerAttribute.Classes.Magician:
                                pa.additionalAtk -= 2;
                                break;
                            case PlayerAttribute.Classes.Warrior:
                                pa.additionalHP -= 10;
                                break;
                        }
                        pa.UpdatePlayerValueByPoint();
                        attibute.GetComponent<Attribute>().UpdatePlayerInfo();
                    }
                    else if (index == 4)
                    {
                        switch (pa.job)
                        {
                            case PlayerAttribute.Classes.Archer:
                                pa.additionalDef -= 2;
                                break;
                            case PlayerAttribute.Classes.Magician:
                                pa.additionalMP -= 10;
                                break;
                            case PlayerAttribute.Classes.Warrior:
                                pa.additionalDef -= 2;
                                break;
                        }
                        pa.UpdatePlayerValueByPoint();
                        attibute.GetComponent<Attribute>().UpdatePlayerInfo();
                    }
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

    public DragAndDropItem FindSkillDADItemByID(int id) {
        foreach (GameObject go in ShortcutItem) {
            DragAndDropItem dadItem = go.GetComponent<DragAndDropItem>();
            if (dadItem.ItemId == id) {
                return dadItem;
            }
        }
        return null;
    }

    public DragAndDropItem GetASkillDaDItem() {
        return new DragAndDropItem();
    }
}
