using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/NewItem")]
public class Item : ScriptableObject
{
    public string Itemname;
    public int ItemNum;
    [TextArea]
    public string ItemInfo;
    public Sprite ItemImg;
    public float Itemdata;//物品數值(已ID來填)
    public int IDtype ;//物品ID(看ItemType)
    public float useValue; //物品消耗hp或mp
    public int weaponCount; //武器使用
    public bool isHead=false;
    public bool isBody=false;
    public bool isShoose=false;
    public bool isFar=false;
    public bool isClose=false;
    public bool isRing=false;
    public enum ItemType
    {
        Other = 0, //其他物品，既不能裝備，也無法消耗
        HeadEquip = 10, //頭部盔甲
        BodyEquip = 11, //身體盔甲
        ShoeEquip = 12, //鞋子
        FarWeapon = 20, //遠程武器
        CloseWeapon = 21, //近戰武器
        RingMagic = 22, //魔法戒指
        HpPotion = 30, //Hp藥水
        MpPotion = 31, //Mp藥水
        AtkPotion = 32, //Atk藥水
        DefPotion = 33, //Def藥水
        InvPotion = 34, //無敵藥水
        SpeedPotion = 35, //速度藥水
        MaxHpAddPotion = 36,//Hp最大值增加
        MaxMpAddPotion = 37//Mp最大值增加
    }
}
