using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTypeNPC : Mission {

    private bool complete;
    private string npc;
    private string scene;

    public MissionTypeNPC(int missionID, int type, string description, string requirement, string npc, string scene, bool complete) : base(missionID, type, description, requirement)
    {
        this.complete = complete;
        this.npc = npc;
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

    public override string Getnpc()
    {
        return npc;
    }

    public override string Getscene()
    {
        return scene;
    }
}
