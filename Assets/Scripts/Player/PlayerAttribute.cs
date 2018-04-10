using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttribute : MonoBehaviour
{
    #region Variable
    public enum Classes { Warrior, Archer, Magician }

    public delegate void LevelUpHandler();
    public event LevelUpHandler LevelUp;
    public UIinfo playerUiScript;
    public Attribute attributeScript;
    public GameObject levelUp;
    public int[] Skill = new int[6];
    public int currentHP;
    public int currentMP;
    public int maxHP;
    public int maxMP;
    public int str;
    public int _int;
    public int agi;
    public int additionalAtk;   //used for add
    public int additionalDef;   //used for add
    public int additionalSpeed; //used for add
    public int additionalHP;    //used for add
    public int additionalMP;    //used for add
    public int atk;             //total included additional
    public int def;             //total included additional
    public float attackSpeed;   //total included additional
    public string playerName;
    public Classes job;
    public int AvailablePoint;
    public int SkillPoint;
    public int currentLevel;
    public int currentExp;
    int totalExp;
    int needExp;
    const int baseExp = 100;
    Animator anim;
    #endregion

    #region LifeCycle
    // Use this for initialization
    void Awake()
    {
        //attribute initialization
        InitialPlayerAttribute();
        //player anim
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Method

    public void TakeDamge(int damage)
    {
        if (StaticVarAndFunction.PlayerIsDead) return;

        currentHP -= ((damage - def) > 1) ? (damage - def) : 1;
        playerUiScript.updateHP(currentHP, maxHP);
        anim.SetTrigger("Damaged");
        if (currentHP <= 0)
        {
            StaticVarAndFunction.PlayerIsDead = true;
            anim.SetTrigger("Dead");
        }
    }

    public void ConsumeMP(int value)
    {
        if (StaticVarAndFunction.PlayerIsDead) return;

        currentMP -= value;
        playerUiScript.updateHP(currentMP, maxMP);
    }

    public void GainExp(int sourceExp, int sourceLevel)
    {
        if (StaticVarAndFunction.PlayerIsDead) return;
        //penalty and bonus for the level difference between player and monster
        //if sourceLevel==0 which is mission, no penaly or bonus will be apply
        float temp = sourceExp;
        bool isLvUp = false;
        temp *= (!(sourceLevel == 0)) ? (((float)(sourceLevel - currentLevel)) / 100f) : 1;
        sourceExp += (int)temp;
        currentExp += sourceExp;
        totalExp += sourceExp;
        //check if level up
        while (currentExp >= needExp)
        {
            currentExp -= needExp;
            float expCoefficient = 1 + (currentLevel / 10) * 0.1f - ((currentLevel % 10) * 0.01f * (currentLevel / 10));
            if (currentLevel < 10)
            {
                expCoefficient = 2 - 0.1f * currentLevel;
            }
            needExp = (int)Mathf.Floor((baseExp * Mathf.Pow(expCoefficient, currentLevel)));
            currentLevel++;
            isLvUp = true;
            AvailablePoint += 5;
            SkillPoint++;
            currentHP = maxHP;
            currentMP = maxMP;
            if (LevelUp != null) LevelUp();
        }
        playerUiScript.updateEXP(currentLevel, currentExp, needExp, isLvUp);
        playerUiScript.updateHP(currentHP, maxHP);
        if (isLvUp) levelUp.SetActive(true);
    }

    //initial
    void InitialPlayerAttribute()
    {
        //playerName = "Fish";
        //job = Classes.Warrior;

        //set name
        //set job,enable character,set staticvarandfunc
        playerName = CreateNewCharacter.nameOfPlayer;
        //switch by name later
        switch (CreateNewCharacter.numOfSelectedCharacter) {
            case 0:
                job = Classes.Warrior;
                break;
            case 1:
                job = Classes.Archer;
                break;
            case 2:
                job = Classes.Magician;
                break;
        }        

        str = 5;
        _int = 5;
        agi = 5;

        additionalAtk = 0;
        additionalDef = 0;
        additionalSpeed = 0;
        additionalHP = 0;
        additionalMP = 0;
        
        UpdatePlayerValueByPoint(); //could be deleted if the save is loaded

        currentLevel = 1;
        currentExp = 0;
        needExp = baseExp;
        AvailablePoint = 0;
        SkillPoint = 0;

        //exp ui update
        playerUiScript.updateEXP(currentLevel, currentExp, needExp, false);

        currentHP = maxHP;
        currentMP = maxMP;
        //hp ui update
        playerUiScript.updateHP(currentHP, maxHP);

        //mp ui update
        playerUiScript.updateMP(currentMP, maxMP);
        Skill.Initialize();

    }

    //call after point change,initial,equipment,skill
    public void UpdatePlayerValueByPoint()
    {
        switch (job)
        {
            case Classes.Warrior:
                atk = 2 + str * 2 + additionalAtk;
                break;
            case Classes.Archer:
                atk = 2 + agi * 2 + additionalAtk;
                break;
            case Classes.Magician:
                atk = 2 + _int * 2 + additionalAtk;
                break;
        }
        def = Mathf.CeilToInt(agi * 0.5f) + additionalDef;
        attackSpeed = 1 + additionalSpeed;    //Todo: Calculate attackSpeed by agi
        maxHP = 100 + str * 8 +additionalHP;
        maxMP = 100 + _int * 5 +additionalMP;
        currentHP = (currentHP < maxHP) ? currentHP : maxHP;
        currentMP = (currentMP < maxMP) ? currentMP : maxMP;

        //update ui and attribute page after value and point update        

        //hp ui update
        playerUiScript.updateHP(currentHP, maxHP);

        //mp ui update
        playerUiScript.updateMP(currentMP, maxMP);

        //update attribute page
        //attributeScript.UpdatePlayerInfo();

    }

    /*
    delegate int PointDekegate(int originalPoint, int playerPoint);
    delegate int ValueDelegate(int originalValue, int playerValue);

    void PointUpdate(PointDekegate pointDelegate) {

    }
    void ValueUpdate(ValueDelegate valueDelegate) {

    }
    */

    //point update
    //equipment update
    //level update
    //add point,value skill apply


    /*
    void CalculatePlayerAttribute()
    {
        ReCalculatePlayerAttribute();

        currentExp = 0;
        currentLevel = 1;
        needExp = baseExp;
        currentHP = maxHP;
        currentMP = maxMP;
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
        def = Mathf.CeilToInt(agi * 0.5f);
        attackSpeed = 1;    //Todo: Calculate attackSpeed by agi
        maxHP = 100 + str * 8;
        maxMP = 100 + _int * 5;
        currentHP = (currentHP > maxHP) ? currentHP : maxHP;
        currentMP = (currentMP > maxMP) ? currentMP : maxMP;
    }
    */

    /*
   public void UpdateAllAttributeInfo()
   {

       //attribute initialization
       ReCalculatePlayerAttribute();

       //exp ui update
       playerUiScript.updateEXP(currentLevel, currentExp, needExp, false);

       //hp ui update
       playerUiScript.updateHP(currentHP, maxHP);

       //mp ui update
       playerUiScript.updateMP(currentMP, maxMP);

       //update attribute page
       attributeScript.UpdatePlayerInfo();

   }
   */
    #endregion
}
