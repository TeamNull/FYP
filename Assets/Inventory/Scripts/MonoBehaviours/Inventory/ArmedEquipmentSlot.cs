using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ArmedEquipmentSlot : MonoBehaviour, IPointerClickHandler{

    int slotId;
    ArmedEquipment armedEquipment;

    // Use this for initialization
    void Start()
    {
        slotId = int.Parse(name);
        armedEquipment = StaticVarAndFunction.armedEquipment;
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
            armedEquipment.RemoveEquipment(slotId);
        }
    }
}
