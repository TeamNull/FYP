﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Equipment : Item
{
       
    public int hp;
    public int mp;
    public int str;
    public int _int;
    public int agi;
    public int atk;
    public int def;
    public float speed;    
    public int equipmentType; //set in unity  
    // 0
    //123
    // 4
    PlayerAttribute pa;
    ArmedEquipment ae;
    Attribute attribute;
    //enum Classes { Warrior, Archer, Magician };
    


    void Start()
    {
        //pa = StaticVarAndFunction.player.GetComponent<PlayerAttribute>();
        //pa = GameObject.FindGameObjectWithTag.pa<PlayerAttribute>();
    }

    public override void ApplyAction() {
        pa = GameManager.player.GetComponent<PlayerAttribute>();
        ae = GameManager.armedEquipment;
        attribute = GameManager.attribute.GetComponent<Attribute>();
        if ((equipmentType == 1) && !(pa.job.Equals(PlayerAttribute.Classes.Warrior) && id == 111 || pa.job.Equals(PlayerAttribute.Classes.Archer) && id == 112 || pa.job.Equals(PlayerAttribute.Classes.Magician) && id == 113)) {
            GameManager.bag.AddItem(this, 1);
            return;
        } 
        //Debug.Log("applyaction in equipment");
        pa.additionalHP += hp;
        pa.currentHP += hp;
        pa.additionalMP += mp;
        pa.currentMP += mp;
        pa.str += str;
        pa.agi += agi;
        pa._int += _int;
        pa.additionalAtk += atk;
        pa.additionalDef += def;
        pa.attackSpeed += speed;
        pa.UpdatePlayerValueByPoint();
        attribute.UpdatePlayerInfo();
        ae.ApplyEquipment(this);
        //Debug.Log("applyaction end in equipment");
    }

    public void RemoveAction() {
        pa = GameManager.player.GetComponent<PlayerAttribute>();
        pa.additionalHP -= hp;
        pa.currentHP -= hp;
        pa.additionalMP -= mp;
        pa.currentMP -= mp;
        pa.str -= str;
        pa.agi -= agi;
        pa._int -= _int;
        pa.additionalAtk -= atk;
        pa.additionalDef -= def;
        pa.attackSpeed -= speed;
        pa.UpdatePlayerValueByPoint();
        attribute.UpdatePlayerInfo();
    }
}
