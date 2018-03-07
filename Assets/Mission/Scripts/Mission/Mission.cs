using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission { 

    private int missionID;
    private int type;
    private string requirement = "nothing";
    private string description ="nothing";
    private string rectname = "nothing"; 
    private string scene = "nothing";
    private string enemyname = "nothing";
    private int countcdienum = 0;



    public Mission(int missionID, int type, string description, string requirement)
    {
        this.missionID = missionID;
        this.type = type;
        this.description = description;
        this.requirement = requirement;
    }

    public int GetmissionID()
    {
        return missionID;
    }

    public int Gettype()
    {
        return type;
    }

    public string Getrequirement()
    {
        return requirement;
    }

    public string Getdescription()
    {
        return description;
    }


    public virtual bool Getcomplete()
    {
        return false;
    }

    public virtual void Setcomplete(bool complete)
    {
    }

    public virtual string Getrectname()
    {
        return rectname;
    }

    public virtual string Getenemyname()
    {
        return enemyname;
    }

    public virtual int Getcountcdienum()
    {
        return countcdienum;
    }

    public virtual string Getscene()
    {
        return scene;
    }


}
