using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttribute : MonoBehaviour
{

    public int maxHP = 100;
    public int maxMP = 100;
    public int nextLvExp = 100;
    public int currentHP;
    public Slider hpSlider;
    public Slider mpSlider;
    public Slider expSlider;


    //for level handling
    int totalExp;
    int currentExp;
    int currentLevel;
    int needExp;
    public Text currentLevelText;
    public Text currentExpText;

    int currentMP;
    bool isDead;
    Animator anim;


    // Use this for initialization
    void Start()
    {
        currentHP = maxHP;
        currentMP = maxMP;
        currentExp = 0;
        currentLevel = 1;
        needExp = 100;
        currentLevelText.text = "LV " + currentLevel.ToString();
        currentExpText.text = currentExp.ToString() + " / " + needExp.ToString() + " ( " + (100 * currentExp / needExp) + "% )";
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamge(int damage)
    {
        if (isDead) return;
        currentHP -= damage;
        hpSlider.value = currentHP;
        anim.SetTrigger("Damaged");
        if (currentHP <= 0)
        {
            isDead = true;
            anim.SetTrigger("Dead");
        }

    }

    public void GainExp(int exp)
    {
        if (isDead) return;

        currentExp += exp;
        totalExp += exp;

        //check if level up
        while (currentExp >= needExp)
        {
            currentExp -= needExp;
            needExp = (int)Mathf.Floor((100 * Mathf.Pow(1.1f, currentLevel)));
            currentLevel++;
            //add 500ms anim on slider
            //add popup
            currentLevelText.text = "LV " + currentLevel;

        }

        expSlider.value = (int)Mathf.Floor((100 * currentExp / needExp));//todo: update slider according to the exp percentage
        currentExpText.text = currentExp + " / " + needExp + " ( " + (100 * currentExp / needExp) + "% )";
    }

    public void ConsumeMP(int value)
    {
        if (isDead) return;

        currentMP -= value;
        mpSlider.value = currentMP;
    }
}
