using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttribute : MonoBehaviour
{
    #region Variable
    enum Classes { Warrior, Archer, Magician }

    public int atk;
    public float attackSpeed;
    
    public Slider hpSlider;
    public Slider mpSlider;
    public Slider expSlider;

    public Text currentLevelText;
    public Text currentExpText;
    public Text levelUpText;

    Classes job = Classes.Warrior;

    Animator anim;
    Animator expAnim;

    int maxHP;
    int maxMP;
    int totalExp;
    int currentExp;
    int currentLevel;
    int needExp;
    int currentHP;
    int currentMP;
    int def = 10;
    int str = 1;
    int _int = 1;
    int agi = 1;
    int distributionAttribute;
    #endregion

#region LifeCycle
    // Use this for initialization
    void Start()
    {
        currentExp = 0;
        currentLevel = 1;
        needExp = 100;
        CalculatePlayerAttribute();
        currentLevelText.text = "LV " + currentLevel.ToString();
        currentExpText.text = currentExp.ToString() + " / " + needExp.ToString() + " ( " + (100 * currentExp / needExp) + "% )";
        anim = GetComponent<Animator>();
        expAnim = GameObject.FindGameObjectWithTag("Exp").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
#endregion

    void CalculatePlayerAttribute() {
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
        maxHP = 100 + str * 8;
        maxMP = 20 + _int * 5;
        currentHP = maxHP;
        currentMP = maxMP;
    }

    public void TakeDamge(int damage)
    {
        if (StaticVarAndFunction.PlayerIsDead) return;
        currentHP -= damage - def;
        hpSlider.value = currentHP;
        anim.SetTrigger("Damaged");
        if (currentHP <= 0)
        {
            StaticVarAndFunction.PlayerIsDead = true;
            anim.SetTrigger("Dead");
        }

    }

    public void GainExp(int exp)
    {
        if (StaticVarAndFunction.PlayerIsDead) return;

        currentExp += exp;
        totalExp += exp;

        //check if level up
        while (currentExp >= needExp)
        {
            currentExp -= needExp;
            needExp = (int)Mathf.Floor((100 * Mathf.Pow(1.1f, currentLevel)));
            currentLevel++;
            //add 500ms anim on slider
            currentLevelText.text = "LV " + currentLevel;
            anim.SetTrigger("LevelUp");//ui anim
            expAnim.SetTrigger("LevelUp");//player anim
            distributionAttribute += 5;
        }

        expSlider.value = (int)Mathf.Floor((100 * currentExp / needExp));//todo: update slider according to the exp percentage
        currentExpText.text = currentExp + " / " + needExp + " ( " + (100 * currentExp / needExp) + "% )";
    }


    public void ConsumeMP(int value)
    {
        if (StaticVarAndFunction.PlayerIsDead) return;

        currentMP -= value;
        mpSlider.value = currentMP;
    }
}
