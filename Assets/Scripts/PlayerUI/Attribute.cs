using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : MonoBehaviour
{
    public UnityEngine.UI.Text playerName, level, job, damage, attackSpeed, str, agi, _int, availablePoint;


    PlayerAttribute pa;

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
        availablePoint.text = pa.AvailablePoint.ToString();        
    }
    
    public void addStr() {
        if (pa.AvailablePoint <= 0) return;
        pa.AvailablePoint -= 1;
        pa.str += 1;
        pa.UpdateAllAttributeInfo();

    }

    public void minusStr()
    {
        if (pa.str <= 0) return;
        pa.AvailablePoint += 1;
        pa.str -= 1;
        pa.UpdateAllAttributeInfo();
    }

    public void add_Int()
    {
        if (pa.AvailablePoint <= 0) return;
        pa.AvailablePoint -= 1;
        pa._int += 1;
        pa.UpdateAllAttributeInfo();
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
        if (pa.AvailablePoint <= 0) return;
        pa.AvailablePoint -= 1;
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
}
