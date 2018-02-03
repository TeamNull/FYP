using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System;
using UnityEditor;

public class Missionboard : MonoBehaviour {

   
    public GameObject originalcanves; // the original canves
    public Inventory inventory; // inventory
    public Button btn; // the button show on the mission canves
    public Text completext; // the text show on the mission canves
    public const int missionnumber = 20; // totoal mission number 

    struct Missiontype
    {
        private string detail; // description of the mission
        private bool start; //the mission started or not
        private bool completeless; // the mission completed or not
        private int itemid; //cannot not be 0 mission item id

        public string Returndetail()
        {
            return detail;
        }

        public bool Returnstart()
        {
            return start;
        }

        public bool Returncompleteless()
        {
            return completeless;
        }

        public int Returnitemid()
        {
            return itemid;
        }

        public void Setdetail(string detail)
        {
            this.detail = detail;
        }

        public void Setstart(bool start)
        {
            this.start = start;
        }

        public void Setcompleteless(bool completeless)
        {
            this.completeless = completeless;
        }

        public void Setitemid(int itemid)
        {
            this.itemid = itemid;
        }
    }

    Missiontype[] missiontype = new Missiontype[missionnumber];

    void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       RaycastHit hit;

        if (missiontype[0].Returnstart()) CheckMissioncompleteless();

       if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
       {
           //Debug.DrawLine(Camera.main.transform.position, hit.transform.position, Color.red, 0.1f, true);
           //Debug.Log(hit.transform.name);
          if (hit.distance <= 5)
          {
             if (hit.transform.name == "missionboard")
             {
                 completext.enabled = true;
                 Missionininitialize();
                 Missionboardsystem();
             }
          }
          

        }

}

    void Missionininitialize()
    {
        missiontype[0].Setdetail("pick up a coffee");
        missiontype[0].Setstart(false);
        missiontype[0].Setcompleteless(false);
        missiontype[0].Setitemid(1);
        return;
    }

    void Missionboardsystem()
    {
    
       
        if (!missiontype[0].Returnstart())
        {
            completext.text = missiontype[0].Returndetail();

            btn.gameObject.SetActive(true);

            btn.onClick.AddListener(Missionstart);

        }

        return;
    }

    void Missionstart()
    {
        btn.gameObject.SetActive(false);
        completext.enabled = false;

        missiontype[0].Setstart(true);

        Debug.Log("hi");
        Debug.Log(inventory.Cotainitem(missiontype[0].Returnitemid()));
 
        
    }

    void CheckMissioncompleteless()
    {
        if (inventory.Cotainitem(missiontype[0].Returnitemid()))
        {
            Debug.Log("hi123");
            missiontype[0].Setcompleteless(true);
            completext.enabled = true;
            completext.text = "This mission is finished";
            return;
        }
    }


}
