using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaveBag : MonoBehaviour
{

    public Text itemName;
    public Text itemInfo;

    public string nameTxt;
    [TextArea]
    public string info;


    public void ItemIsClicked()
    {
        itemName.text = nameTxt;
        itemInfo.text = info;

    }
}
