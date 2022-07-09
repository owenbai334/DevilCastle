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
    int SlotIdtype;
    float Slotvalue;
    public int slotNumber;
    public string SlotName;  
    private static Slot instance;
    public static Slot Instance{
        get
        {
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    public void ItemOnclick()
    {
        InventoryManager.UpdateInfo(SlotInfo);
        InventoryManager.AllInfo(SlotIdtype,Slotvalue,SlotName,slotImage);
        Player.ID = SlotIdtype;
        Player.value = Slotvalue;
        Player.thisItem = slotItem;
        Player.itemnum = slotNum;
    }
    public void SetSlot(Item item)
    {
        if(item==null)
        {
            ItemInslot.SetActive(false);
            return;
        }
        slotItem =item;
        slotNumber = item.ItemNum;
        slotImage.sprite = item.ItemImg;
        slotNum.text = slotNumber.ToString();
        if((int)item.IDtype/10==1||(int)item.IDtype/10==2)
        {
            slotNum.text = null;
        }
        SlotInfo = item.ItemInfo;
        SlotIdtype = item.IDtype;
        Slotvalue  =item.Itemdata;
        SlotName = item.Itemname;
    }
}
