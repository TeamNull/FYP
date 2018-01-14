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

    public int atk;
    public int currentLevel;
    public int def = 1;
    public int str = 1;
    public int _int = 1;
    public int agi = 1;
    public int AvailablePoint;
    public float attackSpeed;
    public string playerName;

    public Slider hpSlider;
    public Slider mpSlider;
    public Slider expSlider;

    public Text currentLevelText;
    public Text currentExpText;
    public Text levelUpText;
    public Text hpText;
    public Text mpText;

    public Classes job = Classes.Warrior;

    Animator anim;
    Animator expAnim;

    int maxHP;
    int maxMP;
    int baseExp = 100;
    int totalExp;
    int currentExp;
    int needExp;
    int currentHP;
    int currentMP;
    #endregion

    #region LifeCycle
    // Use this for initialization
    void Awake()
    {
        //name
        playerName = "Fish";
        CalculatePlayerAttribute();
        //exp
        currentExp = 0;
        currentLevel = 1;
        needExp = 100;
        currentLevelText.text = "LV " + currentLevel.ToString();
        currentExpText.text = currentExp.ToString() + " / " + needExp.ToString() + " ( " + (100 * currentExp / needExp) + "% )";
        expSlider.value = (int)Mathf.Floor((100 * currentExp / needExp));
        //hp
        hpText.text = currentHP + " / " + maxHP;
        hpSlider.value = (int)Mathf.Floor((100 * currentHP / maxHP));
        //mp
        mpText.text = currentMP + " / " + maxMP;
        mpSlider.value = (int)Mathf.Floor((100 * currentMP / maxMP));
        
        
        anim = GetComponent<Animator>();
        expAnim = GameObject.FindGameObjectWithTag("Exp").GetComponent<Animator>();
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
    }

    public void TakeDamge(int damage)
    {
        if (StaticVarAndFunction.PlayerIsDead) return;

        currentHP -= ((damage - def) > 1)?(damage - def):1 ;
        hpSlider.value = (int)Mathf.Floor((100 * currentHP / maxHP));
        hpText.text = currentHP + " / " +maxHP;
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
        temp *= (((float)(sourceLevel - currentLevel) + 100f) / 100f);
        sourceExp = (int)temp;
        //sourceExp *= (int)(Mathf.Floor((float)(sourceLevel - currentLevel) + 100)/100);

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
            //add 500ms anim on slider
            currentLevelText.text = "LV " + currentLevel;
            anim.SetTrigger("LevelUp");//ui anim
            expAnim.SetTrigger("LevelUp");//player anim
            AvailablePoint += 5;
            if (LevelUp != null) LevelUp();
        }

        expSlider.value = (int)Mathf.Floor((100 * currentExp / needExp));
        currentExpText.text = currentExp + " / " + needExp + " ( " + (100 * currentExp / needExp) + "% )";
    }


    public void ConsumeMP(int value)
    {
        if (StaticVarAndFunction.PlayerIsDead) return;

        currentMP -= value;
        mpText.text = currentMP + " / " + maxMP;
        mpSlider.value = (int)Mathf.Floor((100 * currentMP / maxMP)); ;
    }
    #endregion
}
