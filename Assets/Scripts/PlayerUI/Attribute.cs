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
        agi.text = pa.agi.ToString();
        _int.text = pa._int.ToString();
        if(!isEditing) tempAvailablePoint = pa.AvailablePoint;
        availablePoint.text = tempAvailablePoint.ToString();        
    }
    
    public void addStr() {
        if (tempAvailablePoint <= 0) return;
        isEditing = true;
        tempAvailablePoint -= 1;
        usedSTR += 1;
        str.text = pa.str.ToString()+"(+"+ usedSTR+")";
    }

    public void minusStr() {
        if (usedSTR <= 0) return;
        usedSTR -= 1;
        tempAvailablePoint += 1;
        str.text = pa.str.ToString() + "(+" + usedSTR + ")";
    }

    public void add_Int()
    {
        if (tempAvailablePoint <= 0) return;
        isEditing = true;
        tempAvailablePoint -= 1;
        used_INT += 1;
        str.text = pa.str.ToString() + "(+" + usedSTR + ")";
    }

    public void minus_Int()
    {
        if (pa._int <= 0) return;
        pa.AvailablePoint += 1;
        pa._int -= 1;
        pa.UpdateAllAttributeInfo();
    }

    public void addAgi()
    {
        if (tempAvailablePoint <= 0) return;
        isEditing = true;
        tempAvailablePoint -= 1;
        str.text = pa.str.ToString() + "(+" + usedSTR + ")";
        pa.agi += 1;
        pa.UpdateAllAttributeInfo();
    }

    public void minusAgi()
    {
        if (pa.agi <= 0) return;
        pa.AvailablePoint += 1;
        pa.agi -= 1;
        pa.UpdateAllAttributeInfo();
    }

    public void confirmPluaAndMinus() {
        pa.AvailablePoint -=(usedSTR+usedAGI+used_INT);
        pa.str += usedSTR;
        pa.agi += usedAGI;
        pa._int += used_INT;
        isEditing = false;
        pa.UpdateAllAttributeInfo();
    }
}
