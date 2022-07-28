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
    public GameObject[] emptySlot;
    public Text ItemInfomation;
    public Image Itemimg;
    public Text[] ItemData; //0 ATK ,1 DEF,2 USE,3 NAME 
    public Image[] Equips; //0近戰 ,1遠程,2戒指,3鞋子,4頭盔,5身體
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
        else if(IDtype==10||IDtype==11)
        {
            instance.ItemData[0].text = "攻擊力:無攻擊力";
            instance.ItemData[1].text = "防禦力:"+IDdata.ToString();
            instance.ItemData[2].text = "使用效果:無法使用";
        }
        else if(IDtype==12)
        {
            instance.ItemData[0].text = "攻擊力:無攻擊力";
            instance.ItemData[1].text = "防禦力:無防禦力";
            instance.ItemData[2].text = "使用效果:穿上之後+"+IDdata.ToString()+"移動速度";
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
                    instance.ItemData[2].text = "使用效果:使用後無敵3秒鐘";
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
    public static void Refresh()
    {
        for(int j=0;j<4;j++)
        {
            for(int i=0;i<instance.slotGrid[j].transform.childCount;i++)
            {
                if(instance.slotGrid[j].transform.childCount==0)
                {
                    break;
                }
                Destroy(instance.slotGrid[j].transform.GetChild(i).gameObject);   
                instance.slotsList.Clear();   
            }
            for(int i=0;i<instance.bag[j].itemList.Count;i++)
            {
                instance.slotsList.Add(Instantiate(instance.emptySlot[j]));
                instance.slotsList[i].transform.SetParent(instance.slotGrid[j].transform);
                instance.slotsList[i].GetComponent<Slot>().slotId = i;
                instance.slotsList[i].GetComponent<Slot>().SetSlot(instance.bag[j].itemList[i]);
            }
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
            if(Id==0||Id==3)
            {
                if(item.ItemNum<999)
                {
                    item.ItemNum+=1;
                }    
                else
                {
                    item.ItemNum=999;
                }      
            }    
            else
            {
                for(int i=0;i<instance.bag[Id].itemList.Count;i++)
                {
                    if(instance.bag[Id].itemList[i]==null)
                    {
                        instance.bag[Id].itemList[i] = item;
                        break;
                    }
                }
            }                
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
        if(item.ItemNum>1&&Id==3)
        {
            item.ItemNum-=1;
            num.text = item.ItemNum.ToString();
            return;
        }
        if (item.ItemNum<=1&&Id==3)
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
            return;
        } 
        else if(Id==1||Id==2)
        {
            for(int i=0;i<instance.bag[Id].itemList.Count;i++)
            {
                
                if(instance.bag[Id].itemList[i]==null)
                {                   
                    continue;
                }
                switch(id)
                {
                    
                    case 10:
                        if(instance.bag[Id].itemList[i].isHead==true)
                        {
                            instance.bag[Id].itemList[i].isHead=false;
                        }
                        break;
                    case 11:
                        if(instance.bag[Id].itemList[i].isBody == true)
                        {
                            instance.bag[Id].itemList[i].isBody =false;
                        }
                        break;
                    case 12:
                        if(instance.bag[Id].itemList[i].isShoose == true)
                        {
                            instance.bag[Id].itemList[i].isShoose =false;
                        }
                        break;
                    case 20:
                        if(instance.bag[Id].itemList[i].isFar == true)
                        {
                            instance.bag[Id].itemList[i].isFar =false;
                        }
                        break;
                    case 21:
                        if(instance.bag[Id].itemList[i].isClose ==true)
                        {
                            instance.bag[Id].itemList[i].isClose =false;
                        }
                        break;
                    case 22:
                        if(instance.bag[Id].itemList[i].isRing == true)
                        {
                            instance.bag[Id].itemList[i].isRing = false;
                        }
                        break;
                }
            }
            switch(id)
            {
                case 10:
                    item.isHead=true;
                    instance.Equips[4].sprite = item.ItemImg;
                    break;
                case 11:
                    item.isBody=true;
                    instance.Equips[5].sprite = item.ItemImg;
                    break;
                case 12:
                    item.isShoose=true;
                    instance.Equips[3].sprite = item.ItemImg;
                    break;
                case 20:
                    item.isFar=true;
                    instance.Equips[1].sprite = item.ItemImg;
                    break;
                case 21:
                    item.isClose=true;
                    instance.Equips[0].sprite = item.ItemImg;
                    break;
                case 22:
                    item.isRing=true;
                    instance.Equips[2].sprite = item.ItemImg;
                    break;
            }
            Refresh();
            
        }  
    }
    public static void TrashItem(Item item,int id)
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

