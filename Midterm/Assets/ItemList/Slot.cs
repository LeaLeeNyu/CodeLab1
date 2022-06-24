using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    //set the slot - which shows in the canvas - basic info
    public Item slotItem;
    public Image slotImage;

    public void ItemOnClick()
    {
        InventoryManager.UpdateDescription(slotItem.itemInfo,slotItem.itemName);
    }
}
