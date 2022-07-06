using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public Inventory bag;
    public GameObject slotGrid;
    //public Slot slotPrefab;
    public GameObject emptySlot;
    public Text ItemInfomation;
    public List<GameObject> slotsList = new List<GameObject>();
    void Awake()
    {
        if(instance!=null)
        {
            Destroy(this);
        }
        instance = this;
    }
    void OnEnable()
    {
        Refresh();
        instance.ItemInfomation.text = "";
    }
    public static void UpdateInfo(string itemInfo)
    {
        instance.ItemInfomation.text = itemInfo;
    }
    // public static void CreateNewItem(Item item)
    // {
    //     Slot newItem = Instantiate(instance.slotPrefab,instance.slotGrid.transform); 
    //     newItem.slotItem = item;
    //     newItem.slotImage.sprite = item.ItemImg;
    //     newItem.slotNum.text = item.ItemNum.ToString();
    // }
    public static void Refresh()
    {
        for(int i=0;i<instance.slotGrid.transform.childCount;i++)
        {
            if(instance.slotGrid.transform.childCount==0)
            {
                break;
            }
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);      
        }
        instance.slotsList.Clear();
        for(int i=0;i<instance.bag.itemList.Count;i++)
        {
            //CreateNewItem(instance.bag.itemList[i]);
            instance.slotsList.Add(Instantiate(instance.emptySlot));
            instance.slotsList[i].transform.SetParent(instance.slotGrid.transform);
            instance.slotsList[i].GetComponent<Slot>().SetSlot(instance.bag.itemList[i]);
        }
    }
    public static void AddNewItem(Item item)
    {
        if(!instance.bag.itemList.Contains(item))
        {
            //instance.bag.itemList.Add(item);
            for(int i=0;i<instance.bag.itemList.Count;i++)
            {
                if(instance.bag.itemList[i]==null)
                {
                    instance.bag.itemList[i] = item;
                    break;
                }
            }
        }
        else
        {
            item.ItemNum+=1;          
        }
        Refresh();     
    }
}
