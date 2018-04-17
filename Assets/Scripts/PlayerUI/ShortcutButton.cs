using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShortcutButton : MonoBehaviour, IPointerClickHandler {
    public int keyIndex;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(keyIndex.ToString())) {
            OnPointerClick(null);
        }
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("ShortcutKey" + keyIndex.ToString());
        GameManager.AudioManager.GetComponent<BGMcontrol>().Playsound("ClickButton");
        int childNum = transform.childCount;
        if (childNum != 0)
        {
            transform.GetChild(0).GetComponent<DragAndDropItem>().PerformAction();

        }
    }
}