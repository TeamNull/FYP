﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttribute : MonoBehaviour {

    public int maxHP = 100;
    public int maxMP = 100;
    public int nextLvExp = 100;
    public int currentHP;
    public Slider hpSlider;
    public Slider mpSlider;
    public Slider expSlider;


    //for level handling
	int totalExp;
	public int currentExp;
    public int currentLevel;
    public int needExp;
    public Text currentLevelText;
    public Text currentExpText;

    int currentMP;
    

	// Use this for initialization
	void Start () {
        currentHP = maxHP;
        currentMP = maxMP;
        currentExp = 0;
        currentLevel = 1;
        needExp = 100;
        currentLevelText.text = "LV " + currentLevel;
        currentExpText.text = currentExp + " / " + needExp;

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void TakeDamge(int damage) {
        currentHP -= damage;
        hpSlider.value = currentHP;

    }

    public void GainExp(int exp) {
        currentExp += exp;
		totalExp += exp;

		//check if level up
		while(currentExp>=needExp){
			currentExp -=needExp;			
			needExp =(int)Mathf.Floor((100 * Mathf.Pow(1.1f , currentLevel)));
            currentLevel++;
            currentLevelText.text = "LV "+currentLevel;
        }

        expSlider.value = (int)Mathf.Floor((100*currentExp/needExp));//todo: update slider according to the exp percentage
        currentExpText.text = currentExp + " / " + needExp;

    }

    public void ConsumeMP(int value) {
        currentMP -= value;
        mpSlider.value = currentMP;
    }


}
