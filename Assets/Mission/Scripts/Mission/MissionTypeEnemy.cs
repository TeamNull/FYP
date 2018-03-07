using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTypeEnemy : Mission {

    private bool complete;
    private string enemyname;
    private int countcdienum;
    private string scene;

    public MissionTypeEnemy(int missionID, int type, string description, string requirement, string enemyname, int countcdienum, string scene, bool complete) : base(missionID, type, description , requirement)
    {
        this.complete = complete;
        this.enemyname = enemyname;
        this.countcdienum = countcdienum;
        this.scene = scene;
    }


    public override bool Getcomplete()
    {
        return complete;
    }

    public override void Setcomplete(bool complete)
    {
        this.complete = complete;
    }

    public override string Getenemyname()
    {
        return enemyname;
    }

    public override int Getcountcdienum()
    {
        return countcdienum;
    }


    public override string Getscene()
    {
        return scene;
    }
}
