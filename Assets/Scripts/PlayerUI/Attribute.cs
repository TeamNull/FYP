using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : MonoBehaviour
{

    public UnityEngine.UI.Text playerName, level, job, damage, attackSpeed, str, agi, _int, availablePoint;

    // Use this for initialization
    void Start()
    {
        PlayerAttribute pa = StaticVarAndFunction.player.GetComponent<PlayerAttribute>();
        playerName.text = pa.playerName;
        level.text = pa.currentLevel.ToString();
        job.text = pa.job.ToString();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
