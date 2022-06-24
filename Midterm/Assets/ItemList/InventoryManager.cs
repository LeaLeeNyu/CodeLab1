using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Attach to Canvas, create the item to the canbas's bag and transfer the item data
public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    //Set the bag to the inventory
    public Inventory myBag;
    public GameObject slotGrid;
    public Slot slotPrefab;
    public Text itemName;
    public Text itemInfo;
    
    // Make the inventory singleton
    private void Awake()
    {
        if(instance != null & instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // At the begining, the item info on the notebook should be blank
        instance.itemInfo.text = "";
        instance.itemName.text = "<Click Item for more>";
    }


    public static void UpdateDescription(string itemDescription,string itemName)
    {
        instance.itemInfo.text = itemDescription;
        instance.itemName.text = itemName;
    }

    //create the item in the bag
    public static void CreateNewItem(Item item)
    {
        // create a new slot, that contain a item and its image and num, 
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position,Quaternion.identity);
        //make the newItem as the child of the grid in game scene
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        //convey the item data to newItem
        newItem.slotItem = item;
        //upload the item image to the item
        newItem.slotImage.sprite = item.itemImage;
    }


}
