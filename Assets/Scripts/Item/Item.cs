using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/NewItem")]
public class Item : ScriptableObject
{
    public string Itemname;
    public int ItemNum;
    [TextArea]
    public string ItemInfo;
    public Sprite ItemImg;
    public bool Equip;
    public enum Items
    {
        Equipment,
        Other
    }
}
