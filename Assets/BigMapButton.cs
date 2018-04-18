using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigMapButton : MonoBehaviour {

    public GameObject BigMap;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BigMapButtonClick() {
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("ClickButton");
        BigMap.SetActive(true);
    }

    public void BigMapCloseButton() {
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("ClickButton");
        BigMap.SetActive(false);
    }
}
