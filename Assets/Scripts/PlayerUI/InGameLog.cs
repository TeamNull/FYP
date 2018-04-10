using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InGameLog : MonoBehaviour {
    const int totalLogNum = 5;
    public List<Transform> logList = new List<Transform>();
    // Use this for initialization
    void Start () {
        for (int i = 0; i < totalLogNum; i++)
        {
            logList.Add(transform.GetChild(i));
            logList[i].GetComponent<Text>().text="";
        }
        logList.OrderBy(x => x.name);
        for (int i = 0; i < totalLogNum; i++)
        {
            AddLog("");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddLog(string logText) {
        for (int i = totalLogNum-1; i > 0; i--)
        {
            logList[i].GetComponent<Text>().text = logList[i-1].GetComponent<Text>().text;            
        }
        logList[0].GetComponent<Text>().text = logText;
    }
}
