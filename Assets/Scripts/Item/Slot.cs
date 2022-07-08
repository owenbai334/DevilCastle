using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotId;
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;
    public GameObject ItemInslot;
    public string SlotInfo;
    public int SlotIdtype;
    public int Slotvalue;
    public string SlotName;
    public void ItemOnclick()
    {
        InventoryManager.UpdateInfo(SlotInfo);
        InventoryManager.AllInfo(SlotIdtype,Slotvalue,SlotName,slotImage);
    }
    public void SetSlot(Item item)
    {
        if(item==null)
        {
            ItemInslot.SetActive(false);
            return;
        }
        slotImage.sprite = item.ItemImg;
        slotNum.text = item.ItemNum.ToString();
        SlotInfo = item.ItemInfo;
        SlotIdtype = item.IDtype;
        Slotvalue  =item.Itemdata;
        SlotName = item.Itemname;
    }
}
