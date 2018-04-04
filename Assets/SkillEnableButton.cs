using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillEnableButton : MonoBehaviour {

    public static bool SkillUpEnabled;
    public Text ButtonText;

    public void SkillUpSwitch() {
        SkillUpEnabled = !SkillUpEnabled;
        ButtonText.text = SkillUpEnabled ? "Complete" : "Add";
    }
}
