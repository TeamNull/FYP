using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatus : MonoBehaviour {

    const float TIMEOUT = 10f;

    public RawImage icon;
    public Slider Hp;
    public Slider Mp;
    public Text Lv;

    float timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= TIMEOUT) {
            timer = 0f;
            this.gameObject.SetActive(false);
        }
	}

    public void UpdateUI(EnemyAttribute ea) {
        this.gameObject.SetActive(true);
        timer = 0f;
        icon.texture = ea.icon;
        Hp.value = (int)Mathf.Floor((100 * ea.currentHealth / ea.startingHealth));
        Hp.transform.GetChild(2).GetComponent<Text>().text = ea.currentHealth.ToString() + " / " + ea.startingHealth.ToString();
        Mp.value = (int)Mathf.Floor((100 * ea.currentMp / ea.startingMp));
        Mp.transform.GetChild(2).GetComponent<Text>().text = ea.currentMp.ToString() + " / " + ea.startingMp.ToString();
        Lv.text = "Lv " + ea.currentLevel;
    }
}
