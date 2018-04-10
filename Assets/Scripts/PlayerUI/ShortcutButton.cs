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
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        Transform item = transform.GetChild(0);
        if (item != null)
        {
            item.GetComponent<DragAndDropItem>().PerformAction();
            Debug.Log("ShortcutKey" + keyIndex.ToString());
        }
    }
}