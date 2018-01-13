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

    void UpdatePlayerInfo() {
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
}
