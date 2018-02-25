using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public Item coin;
    UnityEngine.UI.Text coinNumber;

	// Use this for initialization
	void Start () {
        coinNumber = gameObject.GetComponent<UnityEngine.UI.Text>();
        coinNumber.text = coin.unit.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        coinNumber.text = coin.unit.ToString();
	}
}
