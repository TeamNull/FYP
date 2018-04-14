using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ArmedEquipment : MonoBehaviour
{    
    //public List<Equipment> equipmentList = new List<Equipment>(); //testing use
    //public List<Transform> equipmentUIList = new List<Transform>();
    public Equipment[] equipmentList = new Equipment[5];
    public Transform[] equipmentUIList = new Transform[5];

    Bag bag;
    //int equipmentOnBody;
    const int equipmentSlotsNum = 5; //total slots
    // 0 
    //123
    // 4

    private void Awake()
    {
        GameManager.armedEquipment = this;
    }

    private void Start()
    {
        // GetComponentsInChildren does not guarantee the order so do the sorting by ourselves
        equipmentUIList[0] = transform.GetChild(0);
        for (int i = 0; i < equipmentSlotsNum; i++)
        {
            //equipmentUIList.Add(transform.GetChild(i));
            equipmentUIList[i] = transform.GetChild(i);
        }
        //equipmentUIList.OrderBy(x => x.name);
        bag = GameManager.bag;
    }

    public void ApplyEquipment(Equipment Equipment)
    {
        if (equipmentList[Equipment.equipmentType]!=null) {
            RemoveEquipment(Equipment.equipmentType);
        }
        equipmentList[Equipment.equipmentType] = Equipment;
        equipmentUIList[Equipment.equipmentType].GetComponent<RawImage>().enabled = false;        
        equipmentUIList[Equipment.equipmentType].GetChild(2).GetComponent<Image>().sprite = Equipment.sprite;
        equipmentUIList[Equipment.equipmentType].GetChild(2).GetComponent<Image>().enabled = true;
        equipmentUIList[Equipment.equipmentType].GetChild(1).gameObject.SetActive(true);
        equipmentUIList[Equipment.equipmentType].GetComponent<ArmedEquipmentSlot>().item = Equipment;
        //itemUIList[i].GetComponent<ShopGrid>().item = goods[i];
    }

    public void RemoveEquipment(int bagId)
    {
        equipmentList[bagId].RemoveAction();
        bag.AddItem(equipmentList[bagId],1);

        equipmentList[bagId]=null;
        equipmentUIList[bagId].GetComponent<RawImage>().enabled = true;
        equipmentUIList[bagId].GetChild(2).GetComponent<Image>().enabled = false;
        //equipmentUIList[Equipment.equipmentType].GetChild(2).GetComponent<Image>().sprite = Equipment.sprite;
        equipmentUIList[bagId].GetChild(1).gameObject.SetActive(false);
        equipmentUIList[bagId].GetComponent<ArmedEquipmentSlot>().item = null;

    }

}