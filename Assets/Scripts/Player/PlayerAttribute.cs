using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttribute : MonoBehaviour {

    public int maxHP = 100;
    public int maxMP = 100;
    public int nextLvExp = 100;
    public Slider hpSlider;
    public Slider mpSlider;
    public Slider expSlider;

    int currentHP;
    int currentMP;
    int currentExp;

	// Use this for initialization
	void Start () {
        currentHP = maxHP;
        currentMP = maxMP;
        currentExp = 0;
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
        expSlider.value = currentExp;
    }

    public void ConsumeMP(int value) {
        currentMP -= value;
        mpSlider.value = currentMP;
    }
}
