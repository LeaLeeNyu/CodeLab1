using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Create new list in assest menu
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Inventory")]

public class Inventory : ScriptableObject
{
    //Create a new bag
    public List<Item> itemList = new List<Item>();
}
