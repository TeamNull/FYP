using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ArmedEquipmentSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    , IPointerExitHandler
{

    public GameObject popUp;
    public Item item;
    int slotId;
    ArmedEquipment armedEquipment;

    // Use this for initialization
    void Start()
    {
        slotId = int.Parse(name);
        armedEquipment = GameManager.armedEquipment;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log(slotId);
            if(armedEquipment!=null)armedEquipment.RemoveEquipment(slotId);
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
            popUp = GameObject.FindGameObjectWithTag("PopUp");
            popUp.GetComponentInChildren<Text>().text = item.description;
            popUp.transform.position = this.transform.position;
            popUp.transform.position += (new Vector3(80, -40, 0));
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        popUp = GameObject.FindGameObjectWithTag("PopUp");
        popUp.transform.position += (new Vector3(10000, 10000, 0));
    }
}
