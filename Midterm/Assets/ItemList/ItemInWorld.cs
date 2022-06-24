using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Set the item in the game scene
public class ItemInWorld : MonoBehaviour
{
    public Item thisItem;
    public Inventory playerInventory;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if player collide with the item, add the item into the bag
        if(collision.tag == "player")
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }

    private void AddNewItem()
    {
        //if the inventory do not contain the item that player have
        if (!playerInventory.itemList.Contains(thisItem))
        {
            //add the item
            playerInventory.itemList.Add(thisItem);
            InventoryManager.CreateNewItem(thisItem);
            Debug.Log("Add Item");
        }
        else
        {
            Debug.Log("Already have Item");
            thisItem.itemHeld += 1;
        }
    }



}
