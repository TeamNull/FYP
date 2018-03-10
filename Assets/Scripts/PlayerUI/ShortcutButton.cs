using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortcutButton : MonoBehaviour {
    int key0Counter = 0;
    // Use this for initialization
    void Start () {
        //Debug.Log("ShortcutBtn0");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShortcutBtn()
    {
        Debug.Log("ShortcutKey" + key0Counter++);
    }
}
