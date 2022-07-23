using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="New Inventory",menuName ="Inventory/NewInventory")]
[Serializable]
public class Inventory : ScriptableObject
{
    public List<Item> itemList = new List<Item>();
}
