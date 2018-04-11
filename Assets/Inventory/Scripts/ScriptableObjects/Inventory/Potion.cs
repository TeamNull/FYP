using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Potion : Item {
    public int hp;
    public int mp;
    PlayerAttribute pa;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void ApplyAction()
    {
        pa = GameManager.player.GetComponent<PlayerAttribute>();
        
        //Debug.Log("applyaction in equipment");
        pa.currentHP += hp;
        pa.currentMP += mp;
        pa.UpdatePlayerValueByPoint();
    }
}
