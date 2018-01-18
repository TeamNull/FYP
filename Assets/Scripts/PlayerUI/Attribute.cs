using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : MonoBehaviour
{
    public UnityEngine.UI.Text playerName, level, job, damage, attackSpeed, str, agi, _int, availablePoint;
    
    PlayerAttribute pa;
    int usedSTR = 0;
    int usedAGI = 0;
    int used_INT = 0;
    int tempAvailablePoint=0;
    bool isEditing=false;

    // Use this for initialization
    void Start()
    {
        pa = StaticVarAndFunction.player.GetComponent<PlayerAttribute>();
        pa.LevelUp += UpdatePlayerInfo;
        UpdatePlayerInfo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdatePlayerInfo() {
        
        playerName.text = pa.playerName;
        level.text = pa.currentLevel.ToString();
        job.text = pa.job.ToString();
        damage.text = pa.atk.ToString();    //Todo: Add back equipment damage
        attackSpeed.text = pa.attackSpeed.ToString();
        str.text = pa.str.ToString();
        if (usedSTR > 0) str.text += "(+" + usedSTR + ")";
        agi.text = pa.agi.ToString();
        if (usedAGI > 0) agi.text += "(+" + usedAGI + ")";
        _int.text = pa._int.ToString();
        if (used_INT > 0) _int.text += "(+" + used_INT + ")";
        if (!isEditing)
        {
            tempAvailablePoint = pa.AvailablePoint;
        }
        else {
            tempAvailablePoint += (pa.AvailablePoint - tempAvailablePoint - usedSTR - usedAGI - used_INT);
        }        
        availablePoint.text = tempAvailablePoint.ToString();        
    }
    
    public void addStr() {
        if (tempAvailablePoint <= 0) return;
        isEditing = true;
        tempAvailablePoint -= 1;
        usedSTR += 1;
        str.text = pa.str.ToString()+"(+"+ usedSTR+")";
        availablePoint.text = tempAvailablePoint.ToString();
    }

    public void minusStr() {
        if (usedSTR <= 0) return;
        isEditing = true;
        usedSTR -= 1;
        tempAvailablePoint += 1;
        str.text = pa.str.ToString();
        if (usedSTR > 0) str.text += "(+" + usedSTR + ")";
        availablePoint.text = tempAvailablePoint.ToString();
    }
    
    public void addAgi()
    {
        if (tempAvailablePoint <= 0) return;
        isEditing = true;
        tempAvailablePoint -= 1;
        usedAGI += 1;
        agi.text = pa.agi.ToString() + "(+" + usedAGI + ")";
        availablePoint.text = tempAvailablePoint.ToString();
    }

    public void minusAgi()
    {
        if (usedAGI <= 0) return;
        isEditing = true;
        usedAGI -= 1;
        tempAvailablePoint += 1; ;
        agi.text = pa.agi.ToString();
        if (usedAGI > 0) agi.text += "(+" + usedAGI + ")";
        availablePoint.text = tempAvailablePoint.ToString();
    }

    public void add_Int()
    {
        if (tempAvailablePoint <= 0) return;
        isEditing = true;
        tempAvailablePoint -= 1;
        used_INT += 1;
        _int.text = pa._int.ToString() + "(+" + used_INT + ")";
        availablePoint.text = tempAvailablePoint.ToString();
    }

    public void minus_Int()
    {
        if (used_INT <= 0) return;
        isEditing = true;
        used_INT -= 1;
        tempAvailablePoint += 1;
        _int.text = pa._int.ToString();
        if (used_INT > 0) _int.text += "(+" + used_INT + ")";
        availablePoint.text = tempAvailablePoint.ToString();
    }

    public void confirmPluaAndMinus() {
        pa.AvailablePoint -= (usedSTR+ usedAGI+ used_INT);
        pa.str += usedSTR;
        pa.agi += usedAGI;
        pa._int += used_INT;
        usedSTR = usedAGI = used_INT = 0;
        isEditing = false;
        pa.UpdateAllAttributeInfo();
    }
}
