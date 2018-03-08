using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Equipment : Item
{
    PlayerAttribute pa;

    int hp = 1;
    int mp = 1;
    int str = 1;
    int _int = 1;
    int agi = 1;
    int atk = 1;
    int def = 1;
    float speed = 0;

    void Start()
    {
        //pa = StaticVarAndFunction.player.GetComponent<PlayerAttribute>();
        //pa = GameObject.FindGameObjectWithTag.pa<PlayerAttribute>();
    }

    public override void ApplyAction() {
        pa = StaticVarAndFunction.player.GetComponent<PlayerAttribute>();
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
        //Debug.Log("applyaction end in equipment");
    }
}
