using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    public Inventory[] bag;
    public GameObject[] slotGrid;
    //public Slot slotPrefab;
    public GameObject[] emptySlot;
    public Text ItemInfomation;
    public List<GameObject> slotsList = new List<GameObject>();
    int Id=0;
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
        for(int i=0;i<instance.slotGrid[instance.Id].transform.childCount;i++)
        {
            if(instance.slotGrid[instance.Id].transform.childCount==0)
            {
                break;
            }
            Destroy(instance.slotGrid[instance.Id].transform.GetChild(i).gameObject);   
            instance.slotsList.Clear();   
        }
        for(int i=0;i<instance.bag[instance.Id].itemList.Count;i++)
        {
            //CreateNewItem(instance.bag.itemList[i]);
            instance.slotsList.Add(Instantiate(instance.emptySlot[instance.Id]));
            instance.slotsList[i].transform.SetParent(instance.slotGrid[instance.Id].transform);
            instance.slotsList[i].GetComponent<Slot>().slotId = i;
            instance.slotsList[i].GetComponent<Slot>().SetSlot(instance.bag[instance.Id].itemList[i]);
        }
    }
    public static void AddNewItem(Item item)
    {
        switch((int)item.IDtype/10)
        {
            case 0:
                instance.Id = 0;
                break;
            case 1:
                instance.Id = 1;
                break;
            case 2:
                instance.Id = 2;
                break;
            case 3:
                instance.Id = 3;
                break;
        }
        if(!instance.bag[instance.Id].itemList.Contains(item))
        {
            //instance.bag.itemList.Add(item);
            for(int i=0;i<instance.bag[instance.Id].itemList.Count;i++)
            {
                if(instance.bag[instance.Id].itemList[i]==null)
                {
                    instance.bag[instance.Id].itemList[i] = item;
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
