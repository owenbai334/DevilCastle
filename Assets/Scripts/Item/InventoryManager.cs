using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;
    static Player player;
    public Inventory[] bag;
    public GameObject[] slotGrid;
    //public Slot slotPrefab;
    public GameObject[] emptySlot;
    public Text ItemInfomation;
    public Image Itemimg;
    public Text[] ItemData; //0 ATK ,1 DEF,2 USE,3 NAME 
    public List<GameObject> slotsList = new List<GameObject>();
    public static int Id;
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
    public static void AllInfo(int IDtype,float IDdata,string name ,Image img)
    {
        instance.ItemData[3].text = name;
        instance.Itemimg.sprite = img.sprite;
        if(IDtype==0)
        {
            instance.ItemData[0].text = "攻擊力:無攻擊力";
            instance.ItemData[1].text = "防禦力:無防禦力";
            instance.ItemData[2].text = "使用效果:無法使用";
        }
        else if(IDtype>=10&&IDtype<20)
        {
            instance.ItemData[0].text = "攻擊力:無攻擊力";
            instance.ItemData[1].text = "防禦力:"+IDdata.ToString();
            instance.ItemData[2].text = "使用效果:無法使用";
        }
        else if(IDtype>=20&&IDtype<30)
        {
            instance.ItemData[0].text = "攻擊力:"+IDdata.ToString();
            instance.ItemData[1].text = "防禦力:無防禦力";
            instance.ItemData[2].text = "使用效果:無法使用";
        }
        else if(IDtype>=30)
        {
            instance.ItemData[0].text = "攻擊力:無攻擊力";
            instance.ItemData[1].text = "防禦力:無防禦力";
            switch(IDtype)
            {
                case 30:
                    instance.ItemData[2].text = "使用效果:使用後增加HP"+IDdata.ToString();
                    break;
                case 31:
                    instance.ItemData[2].text = "使用效果:使用後增加MP"+IDdata.ToString();
                    break;
                case 32:
                    instance.ItemData[2].text = "使用效果:使用後增加攻擊力"+IDdata.ToString();
                    break;
                case 33:
                    instance.ItemData[2].text = "使用效果:使用後增加防禦力"+IDdata.ToString();
                    break;
                case 34:
                    instance.ItemData[2].text = "使用效果:使用後無敵";
                    break;
                case 35:
                    instance.ItemData[2].text = "使用效果:使用後速度"+IDdata.ToString();
                    break;
                case 36:
                    instance.ItemData[2].text = "使用效果:使用後MaxHP增加"+IDdata.ToString();
                    break;
                case 37:
                    instance.ItemData[2].text = "使用效果:使用後MaxMP增加"+IDdata.ToString();
                    break;
            }
        }                             
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
        for(int i=0;i<instance.slotGrid[Id].transform.childCount;i++)
        {
            if(instance.slotGrid[Id].transform.childCount==0)
            {
                break;
            }
            Destroy(instance.slotGrid[Id].transform.GetChild(i).gameObject);   
            instance.slotsList.Clear();   
        }
        for(int i=0;i<instance.bag[Id].itemList.Count;i++)
        {
            //CreateNewItem(instance.bag.itemList[i]);
            instance.slotsList.Add(Instantiate(instance.emptySlot[Id]));
            instance.slotsList[i].transform.SetParent(instance.slotGrid[Id].transform);
            instance.slotsList[i].GetComponent<Slot>().slotId = i;
            instance.slotsList[i].GetComponent<Slot>().SetSlot(instance.bag[Id].itemList[i]);
        }
    }
        // n
    public static void AddNewItem(Item item)
    {
        switch((int)item.IDtype/10)
        {
            case 0:
                Id = 0;
                break;
            case 1:
                Id = 1;
                break;
            case 2:
                Id = 2;
                break;
            case 3:
                Id = 3;
                break;
        }
        if(!instance.bag[Id].itemList.Contains(item))
        {
            //instance.bag.itemList.Add(item);
            for(int i=0;i<instance.bag[Id].itemList.Count;i++)
            {
                if(instance.bag[Id].itemList[i]==null)
                {
                    instance.bag[Id].itemList[i] = item;
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
    public static void UseItem(Item item ,Text num,int id)
    {
        switch((int)item.IDtype/10)
        {
            case 1:
                Id = 1;
                break;
            case 2:
                Id = 2;
                break;
            case 3:
                Id = 3;
                break;
        }
        if(item.ItemNum>1)
        {
            item.ItemNum-=1;
            num.text = item.ItemNum.ToString();
        }
        else if (item.ItemNum<=1)
        {
            for(int i=0;i<instance.bag[Id].itemList.Count;i++)
            {

                if(instance.bag[Id].itemList[i]==item)
                {
                    instance.bag[Id].itemList[i] = null;
                    break;
                }
            }
            Refresh();
        }  
    }
}
