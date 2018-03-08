using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ArmedEquipment : MonoBehaviour
{

    const int equipmentSlotsNum = 5; //total slots
    // 0 
    //123
    // 4
    public List<Equipment> equipmentList = new List<Equipment>(); //testing use
    public List<Transform> equipmentUIList = new List<Transform>();

    int equipmentOnBody;

    private void Awake()
    {
        StaticVarAndFunction.armedEquipment = this;
    }

    private void Start()
    {
        // GetComponentsInChildren does not guarantee the order so do the sorting by ourselves
        for (int i = 0; i < equipmentSlotsNum; i++)
        {
            equipmentUIList.Add(transform.GetChild(i));
        }
        equipmentUIList.OrderBy(x => x.name);
    }

    public void ApplyEquipment(Equipment Equipment, int unit)
    {
        if (equipmentOnBody == equipmentSlotsNum) return; //Check bag space

        for (int i = 0; i < equipmentOnBody; i++) // loop the inventory
        {
            if (equipmentList[i].id == Equipment.id)  // find the item in inventory
            {
                equipmentList[i].UpdateUnit(unit);
                equipmentUIList[i].GetChild(1).GetComponent<Text>().text = Equipment.unit.ToString();
                return;
            }
        }

        equipmentList.Add(Equipment);
        equipmentList[equipmentOnBody].unit++;
        equipmentUIList[equipmentOnBody].GetChild(0).GetComponent<Image>().sprite = Equipment.sprite;
        equipmentUIList[equipmentOnBody].GetChild(0).GetComponent<Image>().enabled = true;
        equipmentUIList[equipmentOnBody].GetChild(1).GetComponent<Text>().text = Equipment.unit.ToString();
        equipmentUIList[equipmentOnBody].GetChild(1).GetComponent<Text>().enabled = true;
        equipmentOnBody++;
    }

    public void RemoveEquipment(int bagId)
    {
        if (equipmentOnBody == 0 || bagId + 1 > equipmentOnBody) return;

        if (StaticVarAndFunction.inventory != null)
        {
            if (StaticVarAndFunction.inventory.transform.parent.gameObject.activeSelf == true)
            {
                StaticVarAndFunction.inventory.AddItem(equipmentList[bagId], 1);
            }
        }

        equipmentList[bagId].ApplyAction();
        equipmentList[bagId].unit--;
        if (equipmentList[bagId].unit == 0)
        {
            equipmentOnBody--;
            equipmentList.RemoveAt(bagId);
        }
        UpdateImageList();
    }

    void UpdateImageList()
    {
        for (int i = 0; i < equipmentSlotsNum; i++)
        {
            if (i < equipmentOnBody)
            {
                equipmentUIList[i].GetChild(0).GetComponent<Image>().sprite = equipmentList[i].sprite;
                equipmentUIList[i].GetChild(0).GetComponent<Image>().enabled = true;
                equipmentUIList[i].GetChild(1).GetComponent<Text>().text = equipmentList[i].unit.ToString();
                equipmentUIList[i].GetChild(1).GetComponent<Text>().enabled = true;
            }
            else
            {
                equipmentUIList[i].GetChild(0).GetComponent<Image>().sprite = null;
                equipmentUIList[i].GetChild(0).GetComponent<Image>().enabled = false;
                equipmentUIList[i].GetChild(1).GetComponent<Text>().text = "0";
                equipmentUIList[i].GetChild(1).GetComponent<Text>().enabled = false;
            }
        }
    }

    public bool CotainEquipment(int itemId)
    {
        for (int itemBagId = 0; itemBagId < equipmentSlotsNum; itemBagId++) // loop the inventory
        {
            if (equipmentList[itemBagId].id == itemId)
                return true;
        }

        return false;
    }
}