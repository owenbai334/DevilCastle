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
    public int IDtype ;
    public enum ItemType
    {
        Other = 0, //其他物品，既不能裝備，也無法消耗
        HeadEquip = 10, //頭部盔甲
        BodyEquip = 11, //身體盔甲
        ShoeEquip = 12, //鞋子
        FarWeapon = 20, //遠程武器
        CloseWeapon = 21, //進戰武器
        RingMagic = 22, //魔法戒指
        HpPotion = 30, //Hp藥水
        MpPotion = 31, //Mp藥水
        AtkPotion = 32, //Atk藥水
        DefPotion = 33, //Def藥水
        InvPotion = 34, //無敵藥水
        SpeedPotion = 35 //速度藥水
    }
}
