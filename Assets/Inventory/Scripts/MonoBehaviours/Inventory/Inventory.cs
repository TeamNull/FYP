using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using System;
using UnityEditor;

public class Inventory : MonoBehaviour {

	public Item[] itemlist = new Item[4]; //testing use
    public const int numItemSlots = 20; //total solts
    public Image[] itemImages = new Image[4]; //show in ui

	struct itemidanditemunit{
		private int itemid; 
		private int itemunit;

		public void setidandunit(int itemid, int itemunit)
		{
			this.itemid = itemid;
			this.itemunit = itemunit;
			return;
		}

		public void updateunit(int increaseunit)
		{
			
			this.itemunit += increaseunit;
			return;
		}
			
		public int returnitemid()
		{
			return itemid;

		}

		public int returnitemunit()
		{
			return itemunit;

		}

		public void setunitas0()
		{
			itemunit = 0;
			return;
		}

		public bool cointainid(int itemid) 
		{
			if (this.itemid == itemid)
				return true;

			return false;
		}
			

	}

	itemidanditemunit[] idandunit = new itemidanditemunit[numItemSlots]; // <itemid , unit>

	public void AddItem (int itemid)
	{	

		for (int iteminventoryid = 0; iteminventoryid < numItemSlots; iteminventoryid++) // loop the inventory
		{ 

			if (idandunit[iteminventoryid].cointainid(itemid))  // find the item in inventory
			{ 

				idandunit [iteminventoryid].updateunit (1); // add 1 to the unit
				itemImages [iteminventoryid].sprite = itemlist [itemid].sprite; // updata the screen ui with the picture
				itemImages [iteminventoryid].enabled = true; // Disable because do not want to show background image
				return;
			}
		}
				 
		for (int iteminventoryid = 0; iteminventoryid < numItemSlots; iteminventoryid++) // loop the inventory
		{ 
			if (idandunit [iteminventoryid].returnitemunit() == 0)  // find which itemslot is empty
			{
				idandunit [iteminventoryid] = new itemidanditemunit (); // initialize the idandunit

				idandunit [iteminventoryid].setidandunit (itemid, 1); // add the item into temp and unit set to 1 because there is 1 item

				itemImages [iteminventoryid].sprite = itemlist [itemid].sprite; // update the screen ui with the picture
				itemImages [iteminventoryid].enabled = true; // Disable because do not want to show background image
				return;
			}
				
		}
			
		return;
			
    }
		
	public void RemoveItem(int inventoryid)
    {


		if (idandunit [inventoryid].returnitemunit() > 1) // 2-1 = 1 still have 1
		{
			idandunit [inventoryid].updateunit (-1); // decrease 1 unit
			return;
		}

		if (idandunit [inventoryid].returnitemunit() == 1) // 1-1 = 0 no more so remove
		{	
			idandunit [inventoryid].setunitas0(); //set the unit as 0
			itemImages[inventoryid].sprite = null; //update the screen ui with the null
			itemImages[inventoryid].enabled = false; // Disable because do not want to show background image
			return;
		}

		return;	
	}

	public bool cotainitem(int itemid)
	{
        for (int iteminventoryid = 0; iteminventoryid < numItemSlots; iteminventoryid++) // loop the inventory
        {
            if (idandunit[iteminventoryid].cointainid(itemid))
                return true;
        }

        return false;
	}
		
		


}


		