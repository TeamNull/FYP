using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttribute : MonoBehaviour
{
    #region Variable
    public delegate void LevelUpHandler();
    public event LevelUpHandler LevelUp;

    public enum Classes { Warrior, Archer, Magician }

    public const string id = "BF1D24BE7DF041E4A40170B1E940BBD4";
    public int atk;
    public int currentLevel;
    public int def = 1;
    public int str = 1;
    public int _int = 1;
    public int agi = 1;
    public int AvailablePoint;
    public int currentExp;
    public int currentHP;
    public int currentMP;
    public int maxHP;
    public int maxMP;
    public float attackSpeed;
    public string playerName;
    
    public UIinfo playerUiScript;
    public Attribute attributeScript;

    public Classes job = Classes.Warrior;

    Animator anim;
    Animator expAnim;

    int baseExp = 100;
    int totalExp;
    int needExp;

    #endregion

    #region LifeCycle
    // Use this for initialization
    void Awake()
    {
        //name
        playerName = "Fish";

        //attribute initialization
        CalculatePlayerAttribute();
        
        //exp
        playerUiScript.updateEXP(currentLevel, currentExp, needExp, false);
        
         //hp
        playerUiScript.updateHP(currentHP, maxHP);
        
        //mp
        playerUiScript.updateMP(currentMP, maxMP);
        
        //player anim
        anim = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Method
    void CalculatePlayerAttribute()
    {
        switch (job)
        {
            case Classes.Warrior:
                atk = 2 + str * 2;
                break;
            case Classes.Archer:
                atk = 2 + agi * 2;
                break;
            case Classes.Magician:
                atk = 2 + _int * 2;
                break;
        }
        attackSpeed = 1;    //Todo: Calculate attackSpeed by agi
        maxHP = 1 + str * 8;
        maxMP = 20 + _int * 5;
        currentHP = maxHP;
        currentMP = maxMP;
        currentExp = 0;
        currentLevel = 1;
        needExp = baseExp;

    }

    void ReCalculatePlayerAttribute()
    {
        switch (job)
        {
            case Classes.Warrior:
                atk = 2 + str * 2;
                break;
            case Classes.Archer:
                atk = 2 + agi * 2;
                break;
            case Classes.Magician:
                atk = 2 + _int * 2;
                break;
        }
        //attackSpeed = 1;    //Todo: Calculate attackSpeed by agi
        maxHP = 1 + str * 8;
        maxMP = 20 + _int * 5;
        currentHP = (currentHP> maxHP) ?maxHP:currentHP;
        currentMP = (currentMP > maxMP) ? maxMP : currentMP;  

    }



    public void TakeDamge(int damage)
    {
        if (StaticVarAndFunction.PlayerIsDead) return;

        currentHP -= ((damage - def) > 1)?(damage - def):1 ;        
        playerUiScript.updateHP(currentHP, maxHP);
        anim.SetTrigger("Damaged");
        if (currentHP <= 0)
        {
            StaticVarAndFunction.PlayerIsDead = true;
            anim.SetTrigger("Dead");
        }

    }

    public void GainExp(int sourceExp, int sourceLevel)
    {
        if (StaticVarAndFunction.PlayerIsDead) return;
        //penalty and bonus for the level difference between player and monster
        //if sourceLevel==0 which is mission, no penaly or bonus will be apply
        float temp = sourceExp;
        bool isLvUp = false;
        temp *= (((float)(sourceLevel - currentLevel)) / 100f);
        sourceExp += (int)temp;
        currentExp += sourceExp;
        totalExp += sourceExp;
        //check if level up
        while (currentExp >= needExp)
        {
            currentExp -= needExp;
            float expCoefficient = 1 + (currentLevel / 10) * 0.1f - ((currentLevel % 10) * 0.01f * (currentLevel / 10));
            if (currentLevel<10) {
                expCoefficient = 2 - 0.1f * currentLevel;
            };
            needExp = (int)Mathf.Floor((baseExp * Mathf.Pow(expCoefficient, currentLevel)));
            currentLevel++;
            isLvUp = true;
            AvailablePoint += 5;
            if (LevelUp != null) LevelUp();
        }
        
        if (isLvUp) anim.SetTrigger("LevelUp");
        playerUiScript.updateEXP(currentLevel, currentExp, needExp, isLvUp);
    }


    public void ConsumeMP(int value)
    {
        if (StaticVarAndFunction.PlayerIsDead) return;

        currentMP -= value;
        playerUiScript.updateHP(currentMP, maxMP);
    }

    public void UpdateAllAttributeInfo() {

        //attribute initialization
        ReCalculatePlayerAttribute();

        //exp
        playerUiScript.updateEXP(currentLevel, currentExp, needExp, false);
        Debug.Log("call updateEXP");

        //hp
        playerUiScript.updateHP(currentHP, maxHP);

        //mp
        playerUiScript.updateMP(currentMP, maxMP);

        attributeScript.UpdatePlayerInfo();

    }
    #endregion
}
