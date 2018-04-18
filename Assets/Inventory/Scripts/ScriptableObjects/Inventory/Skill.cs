using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Skill : Item
{
    public int skillIndex;
    public int skillLevel;

    PlayerAttack pa;

    // Use this for initialization
    void Start()
    {
        pa = GameManager.player.GetComponent<PlayerAttack>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void ApplyAction()
    {
        pa.UseSkill(skillIndex);
    }
}
