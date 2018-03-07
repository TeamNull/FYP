using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionTypeLocation : Mission {

    private bool complete;
    private string rectname;
    private string scene;

    public MissionTypeLocation(int missionID, int type, string description, string requirement, string rectname, string scene, bool complete) : base(missionID, type, description, requirement)
    {
        this.complete = complete;
        this.rectname = rectname;
        this.scene = scene;
    }


    public override  bool Getcomplete()
    {
        return complete;
    }

    public override void Setcomplete(bool complete)
    {
        this.complete = complete;
    }

    public override string Getrectname()
    {
        return rectname;
    }

    public override string Getscene()
    {
        return scene;
    }

}
