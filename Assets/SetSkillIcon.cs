using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSkillIcon : MonoBehaviour {

    public Skill[] skillIconList;

	// Use this for initialization
	void Start () {
        int iconIndex = 1;
        switch (GameManager.player.GetComponent<PlayerAttribute>().job) {
            case PlayerAttribute.Classes.Archer:
                iconIndex = 0;
                break;
            case PlayerAttribute.Classes.Magician:
                iconIndex = 1;
                break;
            case PlayerAttribute.Classes.Warrior:
                iconIndex = 2;
                break;
        }
        gameObject.GetComponent<Image>().sprite = skillIconList[iconIndex].sprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
