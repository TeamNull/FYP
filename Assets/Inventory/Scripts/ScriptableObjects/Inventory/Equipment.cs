using System.Collections;
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

    void Start()
    {
        //pa = StaticVarAndFunction.player.GetComponent<PlayerAttribute>();
        //pa = GameObject.FindGameObjectWithTag.pa<PlayerAttribute>();
    }

    public override void ApplyAction() {
        pa = GameManager.player.GetComponent<PlayerAttribute>();
        ae = GameManager.armedEquipment;
        if ((equipmentType ==1)&& (pa.job.Equals("Warrior")&&id==111||pa.job.Equals("Archer")&&id==112|| pa.job.Equals("Magician") && id == 113)) return;
        //Debug.Log("applyaction in equipment");
        pa.maxHP += hp;
        pa.currentHP += hp;
        pa.maxMP += mp;
        pa.currentMP += mp;
        pa.str += str;
        pa.agi += agi;
        pa._int += _int;
        pa.additionalAtk += atk;
        pa.additionalDef += def;
        pa.attackSpeed += speed;
        pa.UpdatePlayerValueByPoint();
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
    }
}
