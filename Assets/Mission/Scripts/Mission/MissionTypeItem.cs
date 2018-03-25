using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTypeItem : Mission {

    private bool complete;
    private int itemid;
    private string scene;

    public MissionTypeItem(int missionID, int type, string description, string requirement, int itemid, string scene, bool complete) : base(missionID, type, description, requirement)
    {
        this.complete = complete;
        this.itemid = itemid;
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

    public override int Getitemid()
    {
        return itemid;
    }

    public override string Getscene()
    {
        return scene;
    }
}
