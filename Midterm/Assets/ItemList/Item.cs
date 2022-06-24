using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Create new list in assest menu
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]

//ScriptableObject is a class that do not need to attach to a gameObject
public class Item : ScriptableObject
{

    // Set the parameter of the item
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;

    //Create input text area
    [TextArea]
    public string itemInfo;


}
