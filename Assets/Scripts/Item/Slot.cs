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
    public void ItemOnclick()
    {
        InventoryManager.UpdateInfo(SlotInfo);
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
    }
}
